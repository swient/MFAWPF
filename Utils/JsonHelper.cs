﻿using System.IO;
using MFAWPF.Utils.Converters;
using Newtonsoft.Json;

namespace MFAWPF.Utils;

public static class JsonHelper
{
    /// <summary>
    /// 将对象序列化为指定的文件名
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="file"></param>
    /// <param name="defaultS"></param>
    /// <returns></returns>
    ///    //序列化到文件
    public static T? ReadFromConfigJsonFile<T>(string file, T? defaultS = default, bool show = false)
    {
        // 从文件中读取 JSON 字符串
        string directory = $"{AppDomain.CurrentDomain.BaseDirectory}/config/{file}.json";
        // 将 JSON 字符串转换为对象
        try
        {
            if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}/config"))
                Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}/config");
            string jsonString = File.ReadAllText(directory);
            T? result = JsonConvert.DeserializeObject<T>(jsonString) ?? defaultS;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            if (show)
                LoggerService.LogError(e);
            return defaultS;
        }
    }


    public static void WriteToConfigJsonFile(string file, object? content)
    {
        if (content == null) return;
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
        // var dir = Directory.GetCurrentDirectory();
        string jsonString = JsonConvert.SerializeObject(content, settings);
        string directory = $"{AppDomain.CurrentDomain.BaseDirectory}/config/{file}.json";
        if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}/config"))
            Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}/config");
        directory = Path.GetFullPath(directory);
        // 将 JSON 字符串写入文件
        File.WriteAllText(directory, jsonString);
    }

    public static T? ReadFromJsonFile<T>(string file, T? defaultS = default)
    {
        string directory = $"{AppDomain.CurrentDomain.BaseDirectory}/{file}.json";
        try
        {
            if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}"))
                Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}");
            string jsonString = File.ReadAllText(directory);
            // 创建 JsonSerializerSettings 并添加转换器
            var settings = new JsonSerializerSettings();

            return JsonConvert.DeserializeObject<T>(jsonString) ?? defaultS;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LoggerService.LogError(e);
            return defaultS;
        }
    }

    public static void WriteToJsonFile(string file, object content)
    {
        // var dir = Directory.GetCurrentDirectory();
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
        string jsonString = JsonConvert.SerializeObject(content, settings);
        string directory = $"{AppDomain.CurrentDomain.BaseDirectory}/{file}.json";
        if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}"))
            Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}");
        directory = Path.GetFullPath(directory);
        // 将 JSON 字符串写入文件
        File.WriteAllText(directory, jsonString);
    }

    public static T? ReadFromJsonFilePath<T>(string path, string file, T? defaultS = default, Action? action = null)
    {
        if (string.IsNullOrWhiteSpace(path))
            path = AppDomain.CurrentDomain.BaseDirectory;
        string directory = $"{path}/{file}.json";
        try
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string jsonString = File.ReadAllText(directory);
            return JsonConvert.DeserializeObject<T>(jsonString) ?? defaultS;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            action?.Invoke();
            return defaultS;
        }
    }

    public static void WriteToJsonFilePath(string path, string file, object? content)
    {
        if (string.IsNullOrWhiteSpace(path))
            path = AppDomain.CurrentDomain.BaseDirectory;
        // var dir = Directory.GetCurrentDirectory();
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
        string jsonString = JsonConvert.SerializeObject(content, settings);
        string directory = $"{path}/{file}.json";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        directory = Path.GetFullPath(directory);
        // 将 JSON 字符串写入文件
        File.WriteAllText(directory, jsonString);
    }
}