using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DatabaseImporter�̃}�b�s���O�ΏۃC���^�[�t�F�[�X
/// </summary>
public interface IImportable
{
    /// <summary>
    /// �}�b�s���O�Ώۂ̃e�[�u�������擾���܂��B
    /// </summary>
    /// <returns></returns>
    string GetTableName();
    /// <summary>
    /// �}�b�s���O�Ώۂ̌^���擾���܂��B
    /// </summary>
    /// <returns></returns>
    System.Type GetImplType();
}
