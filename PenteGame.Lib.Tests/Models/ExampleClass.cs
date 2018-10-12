using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteGame.Lib.Tests.Models
{
    //we mark the class containing the tests with the TestFixture attribute
    [TestFixture]
    public class ExampleClass
    {
        //we mark the tests with the test attribute
        [Test]
        public void TheFirstTest()
        {
            //if a statement is true
            Assert.True(true);
            
            //if a statement is false
            Assert.False(false);
            
            //if you want to mark something as passed
            Assert.Pass("Passing Message");
            
            //if a value is null
            Assert.IsNull(null);
            
            //if a value is not an instance of a given type
            Assert.IsNotInstanceOf<double>(1);
            
            //if first value is less than second value
            Assert.Less(1, 2);

            //if value one is equal
            Assert.AreEqual(1,1);
            
            //if you want to just fail
            Assert.Fail("Nothing Yet...");
        }
    
        //we mark the tests with the test attribute
        [Test]
        public void TheSecondTest()
        {
            //arrange 
            int expected = 1;
            int actual;

            //act
            actual = DoSomething(expected);

            //assert
            Assert.AreEqual(expected, actual);
            Assert.Fail("Nothing Good....");
        }

        private int DoSomething(int thing)
        {
            return thing;
        }
    }
}
