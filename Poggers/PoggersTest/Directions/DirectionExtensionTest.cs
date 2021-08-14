using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;
using Poggers.Directions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PoggersTest.Directions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DirectionExtensionTest
    {
        [TestMethod]
        public void DirectionWADeconstructsToWAndATest()
        {
            List<Direction> result = Direction.WA.Deconstruct();
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(Direction.W) && result.Contains(Direction.A));
        }

        [TestMethod]
        public void DirectionWDDeconstructsToWAndDTest()
        {
            List<Direction> result = Direction.WD.Deconstruct();
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(Direction.W) && result.Contains(Direction.D));
        }

        [TestMethod]
        public void DirectionSADeconstructsToSAndATest()
        {
            List<Direction> result = Direction.SA.Deconstruct();
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(Direction.S) && result.Contains(Direction.A));
        }

        [TestMethod]
        public void DirectionSDDeconstructsToSAndDTest()
        {
            List<Direction> result = Direction.SD.Deconstruct();
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(Direction.S) && result.Contains(Direction.D));
        }

        [TestMethod]
        [DataRow(Direction.W)]
        [DataRow(Direction.A)]
        [DataRow(Direction.S)]
        [DataRow(Direction.D)]
        public void DirectionCantBeDeconstructedTest(Direction toDeconstruct)
        {
            Assert.IsTrue(toDeconstruct.Deconstruct().Count == 0);
        }

        [TestMethod]
        public void DirectionWVectorIsEqualToZeroAndOneTest()
        {
            Vector2i expected = (0, 1);
            Vector2i result = Direction.W.GetDirectionVector();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DirectionAVectorIsEqualToNegativeOneAndZeroTest()
        {
            Vector2i expected = (-1, 0);
            Vector2i result = Direction.A.GetDirectionVector();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DirectionSVectorIsEqualToZeroAndNegativeOneTest()
        {
            Vector2i expected = (0, -1);
            Vector2i result = Direction.S.GetDirectionVector();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DirectionDVectorIsEqualTOneAndZeroTest()
        {
            Vector2i expected = (1, 0);
            Vector2i result = Direction.D.GetDirectionVector();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DirectionWAVectorIsEqualTNegativeOneAndOneTest()
        {
            Vector2i expected = (-1, 1);
            Vector2i result = Direction.WA.GetDirectionVector();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DirectionWDVectorIsEqualTOneAndOneTest()
        {
            Vector2i expected = (1, 1);
            Vector2i result = Direction.WD.GetDirectionVector();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DirectionSAVectorIsEqualTNegativeOneAndNegativeOneTest()
        {
            Vector2i expected = (-1, -1);
            Vector2i result = Direction.SA.GetDirectionVector();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DirectionSDVectorIsEqualTOneAndNegativeOneTest()
        {
            Vector2i expected = (1, -1);
            Vector2i result = Direction.SD.GetDirectionVector();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void UndefindedDirectionVectorThrowsErrorTest()
        {
            Direction undefined = (Direction)(-1);
            Assert.ThrowsException<InvalidOperationException>(() => undefined.GetDirectionVector());
        }

        [TestMethod]
        [DataRow(Direction.W)]
        [DataRow(Direction.A)]
        [DataRow(Direction.S)]
        [DataRow(Direction.D)]
        [DataRow(Direction.WA)]
        [DataRow(Direction.WD)]
        [DataRow(Direction.SA)]
        [DataRow(Direction.SD)]
        public void DirectionVectorNormalizedIsLengthOfOneTest(Direction direction)
        {
            Vector2 result = direction.GetDirectionNormalized();
            Assert.IsTrue(result.Length - 1 < 0.00001f);
        }

        [TestMethod]
        [DataRow(Direction.W)]
        [DataRow(Direction.A)]
        [DataRow(Direction.S)]
        [DataRow(Direction.D)]
        [DataRow(Direction.WA)]
        [DataRow(Direction.WD)]
        [DataRow(Direction.SA)]
        [DataRow(Direction.SD)]
        public void DirectionVectorSkaledToLength5IsReallyLength5Test(Direction direction)
        {
            Vector2 result = direction.GetDirectionWithLength(5);
            Assert.IsTrue(result.Length - 5 < 0.00001f);
        }

        [TestMethod]
        [DataRow(Direction.W)]
        [DataRow(Direction.A)]
        [DataRow(Direction.S)]
        [DataRow(Direction.D)]
        [DataRow(Direction.WA)]
        [DataRow(Direction.WD)]
        [DataRow(Direction.SA)]
        [DataRow(Direction.SD)]
        public void DirectionVectorSkaledToNegativeLengthThrowsArgumentExceptionTest(Direction direction)
        {
            Assert.ThrowsException<ArgumentException>(() => direction.GetDirectionWithLength(-1));
        }

        [TestMethod]
        [DataRow(Direction.W)]
        [DataRow(Direction.A)]
        [DataRow(Direction.S)]
        [DataRow(Direction.D)]
        [DataRow(Direction.WA)]
        [DataRow(Direction.WD)]
        [DataRow(Direction.SA)]
        [DataRow(Direction.SD)]
        public void DirectionVectorSkaledToLength0ThrowsArgumentExceptionTest(Direction direction)
        {
            Assert.ThrowsException<ArgumentException>(() => direction.GetDirectionWithLength(0));
        }
    }
}
