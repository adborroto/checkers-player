using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Tests
{
    [TestClass()]
    public class PositionTests
    {
        [TestMethod()]
        [DataTestMethod]
        [DataRow(0,0,29)]
        [DataRow(0,2,30)]
        [DataRow(7,7,4)]
        [DataRow(4,4,15)]
        public void FromCoorsTest(int row, int column, int position)
        {
            var pos = Position.FromCoors(row,column);
            Assert.AreEqual(pos.Number, position);
        }
    }
}