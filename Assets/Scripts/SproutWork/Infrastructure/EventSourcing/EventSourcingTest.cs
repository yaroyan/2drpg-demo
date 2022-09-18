using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Yaroyan.Game.RPG.Test
{
    public class EventSourcingTest : MonoBehaviour
    {
        private void Start()
        {
            var broker = new EventBroker();
            var character = new Character(broker);
            broker.Command(new ChangeAgeCommand(character, 123));
            int age = broker.Query<int>(new AgeQuery { Target = character });
            foreach (var b in broker.AllEvents) Debug.Log(b);
            broker.UndoLast();
            foreach (var b in broker.AllEvents) Debug.Log(b);
            Debug.Log(age);
        }
    }

    public class InMemoryCharacterRepository
    {
        readonly Dictionary<CharacterId, IEnumerable<IEvent>> streams = new();
        public Character Get(CharacterId id)
        {
            var character = new Character(id);
            if (streams.ContainsKey(id))
            {
                foreach (var @event in streams[id])
                {
                    character.AddEvent(@event);
                }
            }
            return character;
        }

        public void Save(Character character)
        {
            streams[character.CharacterId] = character.Streams;
        }
    }

    public class CurrentState { }

    public class Character
    {
        readonly CharacterId characterId;
        public string Name { get; private set; }
        public CharacterId CharacterId { get => characterId; }
        List<IEvent> streams;
        public IEnumerable<IEvent> Streams { get => streams; }
        readonly CurrentState _currentState = new();

        int age;
        EventBroker broker;
        public Character(EventBroker broker)
        {
            this.broker = broker;
            broker.Commands += BrokerOnCommands;
            broker.Queries += BrokerOnQueries;
        }

        public void AddEvent(IEvent @event)
        {
            switch (@event)
            {
                case NameChangedEvent nameChangedEvent:
                    Apply(nameChangedEvent);
                    break;
            }
            streams.Add(@event);
        }

        void Apply(NameChangedEvent @event)
        {
            Name = @event.Name;
        }

        public Character(CharacterId id)
        {
            characterId = id;
        }

        void BrokerOnQueries(object sender, IQuery e)
        {
            switch (e)
            {
                case AgeQuery query when query.Target == this:
                    query.Result = age;
                    break;
            }
        }

        void BrokerOnCommands(object sender, ICommand e)
        {
            switch (e)
            {
                case ChangeAgeCommand command when command.Target == this && command.Register:
                    broker.AllEvents.Add(new AgeChangedEvent(this, age, command.Age));
                    age = command.Age;
                    break;
            }
        }
    }

    public record NameChangedEvent(string Name) { }

    public record CharacterId(string id) { }

    public class EventBroker
    {
        public IList<IEvent> AllEvents { get; private set; } = new List<IEvent>();
        public event EventHandler<ICommand> Commands;
        public event EventHandler<IQuery> Queries;

        public void Command(ICommand c)
        {
            Commands?.Invoke(this, c);
        }

        public T Query<T>(Query q)
        {
            Queries?.Invoke(this, q);
            return (T)q.Result;
        }

        public void UndoLast()
        {
            switch (AllEvents.LastOrDefault())
            {
                case AgeChangedEvent @event:
                    Command(new ChangeAgeCommand(@event.Target, @event.OldValue) { Register = false });
                    AllEvents.Remove(@event);
                    break;
            }
        }
    }
    public interface IEvent { }
    public class Event : IEvent { }

    public class AgeChangedEvent : Event
    {
        public Character Target;
        public int OldValue, NewValue;
        public AgeChangedEvent(Character target, int oldValue, int newValue)
        {
            Target = target;
            OldValue = oldValue;
            NewValue = newValue;
        }
        public override string ToString()
        {
            return $"Age changed from {OldValue} to {NewValue}.";
        }
    }

    public interface IQuery { }
    public class Query : IQuery
    {
        public object Result;
    }
    public class AgeQuery : Query
    {
        public Character Target;
    }
    public interface ICommand { }
    public class Command : EventArgs, ICommand
    {
        public bool Register = true;
    }
    public class ChangeAgeCommand : Command
    {
        public Character Target;
        public int Age;
        public ChangeAgeCommand(Character target, int age)
        {
            this.Target = target;
            this.Age = age;
        }
    }
}
/**
 * セーブデータをロードする。
 * セーブするまでロード後の操作がロードしたデータに対する変更であるかどうか確定できない。
 * セーブデータ1をロード後にセーブデータ2として保存した場合、セーブデータ2として保存される。
 * したがって、元のセーブデータに影響を及ぼしてはならない。
 * 元のセーブデータの内容を全てコピーする必要があるのか。
 * イベントソーシングではイベントが発行されるたびにイベントストアに保存されるものと考えられる。そのあとにクエリDBに反映させる。
 * 異なるセーブデータとして保存された場合、クエリＤＢをロールバックする必要があるのでは？
 * セーブデータとは異なる概念としてセッションという概念を導入するとどうなるか。
 * 全てのセーブデータは連続したセッションであると定義。
 * Save1 [session1, session3, session7]
 * Save2 [session1, session2, session4, session5, session6]
 * Save3 [session1, session2, session4, session5, session6]
 * 新規にゲームを始めたとき、または、セーブデータをロードしたときにセッションを発行する。セッションはGUID
 * クエリDBの各レコードにはどのセーブのデータであるか判別するためにSaveIdを付与する。
 * セーブするたびにセッションIDは新規に発行する必要があるのでは？
 * session{ saveId }
 * 内部的には新規セーブデータとしてイベントを処理する。
 * 新規セーブとして保存する場合はオートセーブをそのまま利用する
 * 上書きする場合はイベントを再生してクエリを更新しにいく。
 * 集約の再生はどうなる？
 * クエリDBは？　ロールバック問題は解決されないのでは？
 * 集約は集約IDを基に生成される。
 * InMemoryLiteDbEventStore
 * LiteDbEventStore
 * セーブ処理が走るまではInMemoryEventStoreにEventを格納する。セーブ処理時にLiteDBEventStoreにEventStremを保存する。
 * このとき、スナップショットも作成する。その後、InMemoryEventStoreはクリアする。
 * InMemorySqliteQuery
 * ユーザーが変更を保存しない可能性があるためインメモリのQueryDBを構築する。
 * 各イベントにはインデックスとしてシーケンスから連番を振ることにする。
 * スナップショットはイベントのインデックスの最大値をもつ。最大値から差分イベントを取得する。
 * SaveDomain SessionEntity
 * SaveDomainのSessionCollectionのサイズ数+1をインデックスにする？
 * eventstoreはinmemoryにしない？
 * snapshot runtime permanent
 * runtime permanent
 * sessionTable
 * sessionId saveId
 * QueryDBからデータをReadする時に必要な識別子は何か？ SaveId?
 * SaveIdで読み取りしたQueryDBを更新することはできない。
 * Saveされるまでは同一のセーブデータに上書きされるとは限らない。
 * SaveIdでQueryDBからレコードをReadするものとする。
 * ゲーム開始後に複製イベントが生成されて初めてQueryDBにデータがインサートされるためEntityIdによるFindは不可能
 * Event{ EventId, SessionId, Version, OcuuredAt}
 * Gitライクなセーブシステムを考えてみる。
 * コミット Session
 * リポジトリ Save->Merge
 * ブランチ 同一のSaveから派生したSave。しかし、最終的にマージされることはない。
 * リセマラされることを考えてみる
 * QueryDBはロード時点でのレコードの状態のほうが都合がいい。
 * ゲーム実行中のデータはどこに保存するのか？
 * QueryDBを複製する場合はマスタデータも複製することになる。
 * とりあえずパフォーマンスを度外視してtemporarydatabaseを導入してみる。
 * マスタデータとテーブル定義のみ登録したQueryDBを用意する。
 * インメモリに展開する。スナップショットDBからデータをインサートする。
 * AutoSave
 * 
 * 
 * isAvailableOnBattle
 * 
 * Item1
 *  gotItemEvent ver1
 *  gotItemEvent ver2
 *  
 * Item2
 *  newSessionEvent ver1
 *  gotItemEvent ver2
 *  consumedItemEvent ver3
 *  
 */