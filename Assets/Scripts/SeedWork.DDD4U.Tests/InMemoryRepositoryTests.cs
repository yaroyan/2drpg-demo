using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.Repository.InMemoryRepository;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SeedWork.DDD4U.Test
{
    public class InMemoryRepositoryTests
    {
        TestInMemoryRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = new TestInMemoryRepository();
        }

        [Test]
        [Description("リポジトリに集約を保存する。")]
        public void InsertIntoTheRepository()
        {
            var aggregateRoot = new TestAggregateRoot(_repository.NextIdentity(), "");
            _repository.Save(aggregateRoot);
            Assert.That(_repository.FindAll().Any());
        }

        [Test]
        [Description("リポジトリから保存した集約を取り出す。")]
        public void FindInTheRepository()
        {
            var aggreateRoot = new TestAggregateRoot(_repository.NextIdentity(), "");
            _repository.Save(aggreateRoot);
            var something = _repository.Find(aggreateRoot.Id);
            Assert.AreEqual(aggreateRoot, something);
        }

        [Test]
        [Description("リポジトリから存在しない集約のIDの場合にdefaultを取り出す。")]
        public void FindAggregateRootByAnotherIdInTheRepository()
        {
            var aggreateRoot = new TestAggregateRoot(_repository.NextIdentity(), "");
            _repository.Save(aggreateRoot);
            var anotherAggregateRootId = _repository.NextIdentity();
            var something = _repository.Find(anotherAggregateRootId);
            Assert.AreEqual(null, something);
        }

        [Test]
        [Description("リポジトリから集約を削除する。")]
        public void RemoveFromTheRepository()
        {
            var aggreateRoot = new TestAggregateRoot(_repository.NextIdentity(), "");
            _repository.Save(aggreateRoot);
            _repository.Delete(aggreateRoot);
            Assert.That(!_repository.FindAll().Any());
        }

        [Test]
        [Description("リポジトリに保存した集約を更新する。")]
        public void UpdateAggregateInTheRepository()
        {
            var aggreateRoot = new TestAggregateRoot(_repository.NextIdentity(), "");
            _repository.Save(aggreateRoot);
            string name = "newName";
            aggreateRoot.ChangeName(name);
            _repository.Update(aggreateRoot);
            Assert.AreEqual(name, _repository.Find(aggreateRoot.Id).Name);
        }

        [Test]
        [Description("リポジトリから全ての集約を取得する。")]
        public void FindAllAggregateInTheRepository()
        {
            int iteration = 2;
            for(int i = 0; i < iteration; i++)
            {
                _repository.Save(new TestAggregateRoot(_repository.NextIdentity(), ""));
            }
            Assert.AreEqual(iteration, _repository.FindAll().Count());
        }

        [Test]
        [Description("リポジトリから毎回異なるエンティティIDを発行する。")]
        public void GeneratesDifferentIdEachTime()
        {
            Assert.AreNotEqual(_repository.NextIdentity(), _repository.NextIdentity());
        }
    }
}