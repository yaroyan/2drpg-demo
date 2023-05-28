using System.Collections;
using System.Collections.Generic;

/// <summary>
/// DatabaseImporterのマッピング対象インターフェース
/// </summary>
public interface IImportable
{
    /// <summary>
    /// マッピング対象のテーブル名を取得します。
    /// </summary>
    /// <returns></returns>
    string GetTableName();
    /// <summary>
    /// マッピング対象の型を取得します。
    /// </summary>
    /// <returns></returns>
    System.Type GetImplType();
}
