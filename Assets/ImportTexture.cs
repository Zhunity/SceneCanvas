using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEditor;
using System.IO;

/// <summary>
/// 导入Assets文件夹意外的文件
/// </summary>
public static class LoadObjectOutsideAssets 
{
	static bool _copySuccess = false;
	static Action<Object> _callback;
	static string _selectPath;
	static string _fileName;

	static bool _bindImport = false;

	/// <summary>
	/// 打开选中文件夹
	/// </summary>
	/// <param name="callback"></param>
	public static void Load(Action<Object> callback)
	{
		if(!_bindImport)
		{
			EditorApplication.projectChanged += ProjectChange;
			_bindImport = true;
		}
		_copySuccess = false;
		_selectPath = EditorUtility.OpenFilePanel("请选择文件", _selectPath, "*");
		if(string.IsNullOrEmpty(_selectPath))
		{
			return;
		}
		_callback = callback;
		CopyFile(_selectPath);
	}

	/// <summary>
	/// 复制文件
	/// </summary>
	/// <param name="sourceFileName"></param>
	private static void CopyFile(string sourceFileName)
	{
		_fileName = Path.GetFileName(sourceFileName);
		string tempDirectory = Application.dataPath + "/Temp/";

		// 创建文件夹
		if(!Directory.Exists(tempDirectory))
		{
			Directory.CreateDirectory(tempDirectory);
		}
		string destFileName = tempDirectory + _fileName;

		// 替换文件，把旧文件删了
		if(File.Exists(destFileName))
		{
			File.Delete(destFileName);
			AssetDatabase.Refresh();
		}

		File.Copy(sourceFileName, destFileName);
		_copySuccess = true;
		AssetDatabase.Refresh();
	}

	/// <summary>
	/// 导入成功回调
	/// </summary>
	private static void ProjectChange()
	{
		if(!_copySuccess)
		{
			return;
		}
		_copySuccess = false;
		string assetPath = "Assets/Temp/" + _fileName;
		ImportSetting(assetPath);

		Object obj = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
		if(_callback != null)
		{
			_callback(obj);
			_callback = null;
		}
	}

	/// <summary>
	/// 导入设置
	/// 当然这部分也可以放到callback里面，灵活设置各种配置属性
	/// </summary>
	/// <param name="path"></param>
	private static void ImportSetting(string path)
	{
		AssetImporter importer = AssetImporter.GetAtPath(path);
		if(importer == null)
		{
			return;
		}
		// 做一些列处理，这里就不写了，具体资源具体分析
	}

}
