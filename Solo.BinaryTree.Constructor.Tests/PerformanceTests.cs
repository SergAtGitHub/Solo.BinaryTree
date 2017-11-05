using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solo.BinaryTree.Constructor.Infrastructure.Builders;
using Solo.BinaryTree.Constructor.Infrastructure.Traverse;
using Solo.BinaryTree.Constructor.Serializer;

namespace Solo.BinaryTree.Constructor.Tests
{
    [TestClass]
    public class PerformanceTests
    {
        [TestMethod]
        public void Parser_ShouldPerformFast_WhenReadingFromMemory()
        {
            var data = Enumerable.Range(0, int.MaxValue / 1000).Select(x => UpperLettersAndNumericCharacters.Instance.GetRandom(16)).Distinct().ToArray();

            var builder = new LineByLineTreeBuilder();
            builder.AddData(data);
            string input = Api.Serialize(builder.Root, InlineTreeFormatter.WellKnownFormats.CommaAndSpaceSeparated);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Api.BuildTreeByStringInput(input);

            sw.Stop();

            var acceptanceTimeInSeconds = 6;
            Trace.WriteLine(string.Format("Elapsed={0}", sw.Elapsed.Seconds));
            Assert.IsTrue(sw.Elapsed.Seconds <= acceptanceTimeInSeconds);
        }

        [TestMethod]
        public void ReadingFromHardDrive_ShouldNotBeMoreThenThirteenSeconds()
        {
            Queue<string> guids = new Queue<string>();
            guids.Enqueue(Guid.NewGuid().ToString("N"));
            var number = Int32.MaxValue / 1000;

            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\data.txt";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                for (int i = 0; i < number; i++)
                {
                    var left = Guid.NewGuid().ToString("N");
                    var right = Guid.NewGuid().ToString("N");

                    if (guids.Count < 1000)
                    {
                        guids.Enqueue(left);
                        guids.Enqueue(right);
                    }

                    writer.WriteLine(string.Format(InlineTreeFormatter.WellKnownFormats.CommaAndSpaceSeparated, guids.Dequeue(), left, right));
                }
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var tree = Api.BuildTreeByFilePath(path);

            sw.Stop();

            var acceptanceTimeInSeconds = 13;
            Trace.WriteLine(string.Format("Elapsed={0}", sw.Elapsed.Seconds));
            File.Delete(path);

            Assert.IsTrue(sw.Elapsed.Seconds <= acceptanceTimeInSeconds);
        }
    }

    public interface IRandomStringProvider
    {
        string GetRandom(int length);
    }

    public class UpperLettersAndNumericCharacters : IRandomStringProvider
    {
        public static readonly UpperLettersAndNumericCharacters Instance = new UpperLettersAndNumericCharacters();
        private static Random random = new Random();

        public string GetRandom(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
