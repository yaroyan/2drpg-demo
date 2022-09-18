using System.Collections;
using System.Collections.Generic;

/// <summary>
/// コマンドパターンの命令を表現するインターフェース
/// </summary>
public interface Command
{
    /// <summary>
    /// 命令を実行する。
    /// </summary>
    void Execute();
}
