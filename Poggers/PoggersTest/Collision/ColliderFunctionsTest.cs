using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenTK.Mathematics;
using Poggers.Collision;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PoggersTest.Collision
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ColliderFunctionsTest
    {
        private ICollidable GetMockCircle(float centerX, float centerY, float radius)
        {
            Mock<ICollidableCircle> mock = new Mock<ICollidableCircle>();
            mock.SetupGet(x => x.Center).Returns(new Vector2(centerX, centerY));
            mock.SetupGet(x => x.Radius).Returns(radius);
            return mock.Object;
        }

        private ICollidable GetMockRecktangle(float centerX, float centerY, float width, float height)
        {
            Mock<ICollidableRectangle> mock = new Mock<ICollidableRectangle>();
            mock.SetupGet(x => x.Center).Returns(new Vector2(centerX, centerY));
            mock.SetupGet(x => x.Width).Returns(width);
            mock.SetupGet(x => x.Height).Returns(height);
            return mock.Object;
        }

        [TestMethod]
        public void CollisionBetweenIdenticalCircles()
        {
            ICollidable mockCircle = GetMockCircle(0, 0, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockCircle, mockCircle));
        }
      
        [TestMethod]
        public void CollisionBetweenCircleAndCircleXAxis()
        {
            ICollidable mockCircle1 = GetMockCircle(1, 0, 1);
            ICollidable mockCircle2 = GetMockCircle(-1, 0, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockCircle1, mockCircle2));
        }

        [TestMethod]
        public void NoCollisionBetweenCircleAndCircleXAxis()
        {
            ICollidable mockCircle1 = GetMockCircle(1, 0, 0.9f);
            ICollidable mockCircle2 = GetMockCircle(-1, 0, 1);
            Assert.IsFalse(ColliderFunctions.Collides(mockCircle1, mockCircle2));
        }

        [TestMethod]
        public void CollisionBetweenCircleAndCircleYAxis()
        {
            ICollidable mockCircle1 = GetMockCircle(0, 1, 1);
            ICollidable mockCircle2 = GetMockCircle(0, -1, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockCircle1, mockCircle2));
        }

        [TestMethod]
        public void NoCollisionBetweenCircleAndCircleYAxis()
        {
            ICollidable mockCircle1 = GetMockCircle(0, 1, 0.9f);
            ICollidable mockCircle2 = GetMockCircle(0, -1, 1);
            Assert.IsFalse(ColliderFunctions.Collides(mockCircle1, mockCircle2));
        }

        [TestMethod]
        public void CollisionBetweenRectangleAndRectangleQuadrant1()
        {
            ICollidable mockRectangle1 = GetMockRecktangle(0, 0, 1, 1);
            ICollidable mockRectangle2 = GetMockRecktangle(1, 1, 1, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockRectangle1, mockRectangle2));
        }

        [TestMethod]
        public void CollisionBetweenRectangleAndRectangleQuadrant2()
        {
            ICollidable mockRectangle1 = GetMockRecktangle(0, 0, 1, 1);
            ICollidable mockRectangle2 = GetMockRecktangle(1, -1, 1, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockRectangle1, mockRectangle2));
        }

        [TestMethod]
        public void CollisionBetweenRectangleAndRectangleQuadrant3()
        {
            ICollidable mockRectangle1 = GetMockRecktangle(0, 0, 1, 1);
            ICollidable mockRectangle2 = GetMockRecktangle(-1, -1, 1, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockRectangle1, mockRectangle2));
        }

        [TestMethod]
        public void CollisionBetweenRectangleAndRectangleQuadrant4()
        {
            ICollidable mockRectangle1 = GetMockRecktangle(0, 0, 1, 1);
            ICollidable mockRectangle2 = GetMockRecktangle(-1, 1, 1, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockRectangle1, mockRectangle2));
        }

        [TestMethod]
        public void NoCollisionBetweenRectangleAndRectangleQuadrant1()
        {
            ICollidable mockRectangle1 = GetMockRecktangle(0, 0, 1, 1);
            ICollidable mockRectangle2 = GetMockRecktangle(2, 0.1f, 0.9f, 0.9f);
            Assert.IsFalse(ColliderFunctions.Collides(mockRectangle1, mockRectangle2));
        }

        [TestMethod]
        public void NoCollisionBetweenRectangleAndRectangleQuadrant2()
        {
            ICollidable mockRectangle1 = GetMockRecktangle(0, 0, 1, 1);
            ICollidable mockRectangle2 = GetMockRecktangle(0.1f, -2, 0.9f, 0.9f);
            Assert.IsFalse(ColliderFunctions.Collides(mockRectangle1, mockRectangle2));
        }

        [TestMethod]
        public void NoCollisionBetweenRectangleAndRectangleQuadrant3()
        {
            ICollidable mockRectangle1 = GetMockRecktangle(0, 0, 1, 1);
            ICollidable mockRectangle2 = GetMockRecktangle(-1, -1, 0.9f, 0.9f);
            Assert.IsFalse(ColliderFunctions.Collides(mockRectangle1, mockRectangle2));
        }

        [TestMethod]
        public void NoCollisionBetweenRectangleAndRectangleQuadrant4()
        {
            ICollidable mockRectangle1 = GetMockRecktangle(0, 0, 1, 1);
            ICollidable mockRectangle2 = GetMockRecktangle(-1, 1, 0.9f, 0.9f);
            Assert.IsFalse(ColliderFunctions.Collides(mockRectangle1, mockRectangle2));
        }
        

        [TestMethod]
        public void CollisionBetweenIdenticalRectangles()
        {
            ICollidable mockRectangle1 = GetMockRecktangle(0, 0, 1, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockRectangle1, mockRectangle1));
        }

        [TestMethod]
        public void CollisionBetweenCircleAndRectangle()
        {
            ICollidable mockCircle = GetMockCircle(0, 0, 2);
            ICollidable mockRectangle = GetMockRecktangle(1, 1, 1, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockCircle, mockRectangle));
        }

        [TestMethod]
        public void NoCollisionBetweenCircleAndRectangle()
        {
            ICollidable mockCircle = GetMockCircle(0, 0, 0.3f);
            ICollidable mockRectangle = GetMockRecktangle(1, 1, 1, 1);
            Assert.IsFalse(ColliderFunctions.Collides(mockCircle, mockRectangle));
        }

        [TestMethod]
        public void CollisionBetweenRectangleAndCircle()
        {
            ICollidable mockRectangle = GetMockRecktangle(1, 1, 1, 1);
            ICollidable mockCircle = GetMockCircle(0, 0, 2);
            Assert.IsTrue(ColliderFunctions.Collides(mockRectangle, mockCircle));
        }

        [TestMethod]
        public void NoCollisionBetweenRectangleAndCircle()
        {
            ICollidable mockRectangle = GetMockRecktangle(2, 2, 1, 1);
            ICollidable mockCircle = GetMockCircle(0, 0, 1);
            Assert.IsFalse(ColliderFunctions.Collides(mockRectangle, mockCircle));
        }

        [TestMethod]
        public void CollisionBetweenRectangleAndCirclePositiveXAxis()
        {
            ICollidable mockRectangle = GetMockRecktangle(0, 0, 2, 2);
            ICollidable mockCircle = GetMockCircle(2, 0, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockRectangle, mockCircle));
        }

        [TestMethod]
        public void NoCollisionBetweenRectangleAndCirclePositiveXAxis()
        {
            ICollidable mockRectangle = GetMockRecktangle(0, 0, 2, 2);
            ICollidable mockCircle = GetMockCircle(2, 0, 0.9f);
            Assert.IsFalse(ColliderFunctions.Collides(mockRectangle, mockCircle));
        }

        [TestMethod]
        public void CollisionBetweenRectangleAndCircleNegativeXAxis()
        {
            ICollidable mockRectangle = GetMockRecktangle(0, 0, 2, 2);
            ICollidable mockCircle = GetMockCircle(-2, 0, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockRectangle, mockCircle));
        }

        [TestMethod]
        public void NoCollisionBetweenRectangleAndCircleNegativeXAxis()
        {
            ICollidable mockRectangle = GetMockRecktangle(0, 0, 2, 2);
            ICollidable mockCircle = GetMockCircle(-2, 0, 0.9f);
            Assert.IsFalse(ColliderFunctions.Collides(mockRectangle, mockCircle));
        }

        [TestMethod]
        public void CollisionBetweenRectangleAndCirclePositiveYAxis()
        {
            ICollidable mockRectangle = GetMockRecktangle(0, 0, 2, 2);
            ICollidable mockCircle = GetMockCircle(0, 2, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockRectangle, mockCircle));
        }

        [TestMethod]
        public void NoCollisionBetweenRectangleAndCirclePositiveYAxis()
        {
            ICollidable mockRectangle = GetMockRecktangle(0, 0, 2, 2);
            ICollidable mockCircle = GetMockCircle(0, 2, 0.9f);
            Assert.IsFalse(ColliderFunctions.Collides(mockRectangle, mockCircle));
        }

        [TestMethod]
        public void CollisionBetweenRectangleAndCircleNegativeYAxis()
        {
            ICollidable mockRectangle = GetMockRecktangle(0, 0, 2, 2);
            ICollidable mockCircle = GetMockCircle(0, -2, 1);
            Assert.IsTrue(ColliderFunctions.Collides(mockRectangle, mockCircle));
        }

        [TestMethod]
        public void NoCollisionBetweenRectangleAndCircleNegativeYAxis()
        {
            ICollidable mockRectangle = GetMockRecktangle(0, 0, 2, 2);
            ICollidable mockCircle = GetMockCircle(0, -2, 0.9f);
            Assert.IsFalse(ColliderFunctions.Collides(mockRectangle, mockCircle));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ErrorOnNotImplementingICollidableRectangleOrICollidableCircle()
        {
            ICollidable a = null, b = null;
            ColliderFunctions.Collides(a, b);
        }
    }
}
