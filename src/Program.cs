using System;
using System.IO;
using System.IO.Compression;

if (args.Length < 1)
{
    Console.WriteLine("Please provide a command.");
    return;
}

string command = args[0];

if (command == "init")
{

    Directory.CreateDirectory(".git");
    Directory.CreateDirectory(".git/objects");
    Directory.CreateDirectory(".git/refs");
    File.WriteAllText(".git/HEAD", "ref: refs/heads/main\n");
    Console.WriteLine("Initialized git directory");
}
else if (command == "cat-file")
{
    using FileStream fs = new FileStream(".git/objects/{args[2][..2]}/{args[2][..2]}", FileMode.Open, FileAccess.Read);
    
    using ZLibStream zlibStream = new ZLibStream(fs, Zlib.CompressionMode.Decompress);
    
    using StreamReader reader = new StreamReader(zlibStream);

    Console.WriteLine(reader.ReadToEnd().Split('\x00')[1]);
}
else
{
    throw new ArgumentException($"Unknown command {command}");
}