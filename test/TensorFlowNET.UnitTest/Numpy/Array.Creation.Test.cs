﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tensorflow.NumPy;

namespace TensorFlowNET.UnitTest.Numpy
{
    /// <summary>
    /// https://numpy.org/doc/stable/reference/routines.array-creation.html
    /// </summary>
    [TestClass]
    public class NumpyArrayCreationTest : EagerModeTestBase
    {
        [TestMethod]
        public void empty_zeros_ones_full()
        {
            var empty = np.empty((2, 2));
            var zeros = np.zeros((2, 2));
            var ones = np.ones((2, 2));
            var full = np.full((2, 2), 0.1f);
        }

        [TestMethod]
        public void arange()
        {
            var x = np.arange(3);
            AssetSequenceEqual(new[] { 0, 1, 2 }, x.Data<int>());

            x = np.arange(3f);
            Assert.IsTrue(Equal(new float[] { 0, 1, 2 }, x.Data<float>()));

            var y = np.arange(3, 7);
            AssetSequenceEqual(new[] { 3, 4, 5, 6 }, y.Data<int>());

            y = np.arange(3, 7, 2);
            AssetSequenceEqual(new[] { 3, 5 }, y.Data<int>());
        }

        [TestMethod]
        public void array()
        {
            var x = np.array(1, 2, 3);
            AssetSequenceEqual(new[] { 1, 2, 3 }, x.Data<int>());

            x = np.array(new[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            AssetSequenceEqual(new[] { 1, 2, 3, 4, 5, 6 }, x.Data<int>());
        }

        [TestMethod]
        public void eye()
        {
            var x = np.eye(3, k: 1);
            Assert.IsTrue(Equal(new double[] { 0, 1, 0, 0, 0, 1, 0, 0, 0 }, x.Data<double>()));
        }

        [TestMethod]
        public void linspace()
        {
            var x = np.linspace(2.0, 3.0, num: 5);
            Assert.IsTrue(Equal(new double[] { 2, 2.25, 2.5, 2.75, 3 }, x.Data<double>()));

            x = np.linspace(2.0, 3.0, num: 5, endpoint: false);
            Assert.IsTrue(Equal(new double[] { 2, 2.2, 2.4, 2.6, 2.8 }, x.Data<double>()));
        }

        [TestMethod]
        public void meshgrid()
        {
            var x = np.linspace(0, 1, num: 3);
            var y = np.linspace(0, 1, num: 2);
            var (xv, yv) = np.meshgrid(x, y);
            Assert.IsTrue(Equal(new double[] { 0, 0.5, 1, 0, 0.5, 1 }, xv.Data<double>()));
            Assert.IsTrue(Equal(new double[] { 0, 0, 0, 1, 1, 1 }, yv.Data<double>()));

            (xv, yv) = np.meshgrid(x, y, sparse: true);
            Assert.IsTrue(Equal(new double[] { 0, 0.5, 1 }, xv.Data<double>()));
            AssetSequenceEqual(new long[] { 1, 3 }, xv.shape.dims);
            Assert.IsTrue(Equal(new double[] { 0, 1 }, yv.Data<double>()));
            AssetSequenceEqual(new long[] { 2, 1 }, yv.shape.dims);
        }
    }
}