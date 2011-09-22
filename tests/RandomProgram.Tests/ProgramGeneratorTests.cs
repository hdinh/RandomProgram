namespace Dinh.RandomProgram.Tests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Tests for Program Generator.
    /// </summary>
    [TestFixture]
    public class ProgramGeneratorTests
    {
        [Test]
        [Ignore]
        public void BrainStorm() {
            DynamicProgram program = ProgramGenerator.CreateProgram(typeof(Func<int>));
            ////DynamicProgram program = generator.CreateProgram(typeof(int), typeof(int));
            ////generator.Import(".....");
            ////program.Execute();

            ////Program program = generator.Import("myprogram.txt");
            ////program.Export("myprogram.txt");
            ////program.Execute();
            ////program.Execute();
            ////Func<int> created = lc.CreateLambda<Func<int>>();
        }
    }
}
