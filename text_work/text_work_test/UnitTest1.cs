using System;
using text_work;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace text_work_test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void check_changes_1()
        {
            text test = new text();
            string to_test_trev = "my first test for text";
            string to_test = "my second test for text";
            string expected = "my ;;;-3second;;;-4 test for text";
            int extra = 0;
            string result = test.changes(to_test, to_test_trev, ref extra);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void check_changes_2()
        {
            text test = new text();
            string to_test_trev = "my first test for text";
            string to_test = "my first test for textbox";
            string expected = "my first test for text;;;-3box;;;-4";
            int extra = 0;
            string result = test.changes(to_test, to_test_trev, ref extra);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void check_changes_3()
        {
            text test = new text();
            string to_test_trev = "my first test for text";
            string to_test = "my third test for text";
            string expected = "my ;;;-3third;;;-4 test for text";
            int extra = 0;
            string result = test.changes(to_test, to_test_trev, ref extra);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void check_changes_4()
        {
            text test = new text();
            string to_test_trev = "my first test for text";
            string to_test = "my test for text";
            string expected = "my ;;;-3;;;-4test for text";
            int extra = 0;
            string result = test.changes(to_test, to_test_trev, ref extra);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void to_100percent()
        {
            text test = new text();
            string to_test_trev = "my first test for text";
            string to_test = "my first test for mcln";
            string expected = "my first test for ;;;-3mcln;;;-4";
            int extra = 0;
            string result = test.changes(to_test, to_test_trev, ref extra);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_1()
        {
            text test = new text();
            string to_test = ";;;-3u;;;-4my first test for text";
            string to_test_prev = ";;;-3my first;;;-4 test for text";
            string expected = ";;;-3umy first;;;-4 test for text";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_2()
        {
            text test = new text();
            string to_test = ";;;-3u;;;-4y first test for text";
            string to_test_prev = ";;;-3my first;;;-4 test for text";
            string expected = ";;;-3uy first;;;-4 test for text";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_3()
        {
            text test = new text();
            string to_test = ";;;-3u;;;-4 first test for text";
            string to_test_prev = ";;;-3m;;;-4 first test for text";
            string expected = ";;;-3u;;;-4 first test for text";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_4()
        {
            text test = new text();
            string to_test = ";;;-3u;;;-4my first test for text";
            string to_test_prev = "my ;;;-3first;;;-4 test for text";
            string expected = ";;;-3u;;;-4my ;;;-3first;;;-4 test for text";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_5()
        {
            text test = new text();
            string to_test = "my first test for text;;;-3 so;;;-4";
            string to_test_prev = "my ;;;-3first test for text;;;-4";
            string expected = "my ;;;-3first test for text so;;;-4";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_6()
        {
            text test = new text();
            string to_test = "my first test for text;;;-3 so;;;-4";
            string to_test_prev = "my ;;;-3first test for;;;-4 text";
            string expected = "my ;;;-3first test for;;;-4 text;;;-3 so;;;-4";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_7()
        {
            text test = new text();
            string to_test_trev = "my first test for text";
            string to_test = "my ;;;-3first;;;-4 test for text";
            string expected = "my ;;;-3first;;;-4 test for text";
            string result = test.adding_to_other_changings(to_test, to_test_trev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_8()
        {
            text test = new text();
            string to_test = "my first test for te;;;-3sw;;;-4";
            string to_test_prev = "my ;;;-3first test for text;;;-4";
            string expected = "my ;;;-3first test for tesw;;;-4";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_9()
        {
            text test = new text();
            string to_test = "my f;;;-3u;;;-4st test for text";
            string to_test_prev = "my ;;;-3first;;;-4 test for;;;-3 text;;;-4";
            string expected = "my ;;;-3fust;;;-4 test for;;;-3 text;;;-4";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_10()
        {
            text test = new text();
            string to_test = "m;;;-3;;;-4first test for text";
            string to_test_prev = "my ;;;-3first;;;-4 test for;;;-3 text;;;-4";
            string expected = "m;;;-3first;;;-4 test for;;;-3 text;;;-4";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_11()
        {
            text test = new text();
            string to_test = "lll;;;-3ub;;;-4 first test for text";
            string to_test_prev = "lll;;;-3m;;;-4 first test for text";
            string expected = "lll;;;-3ub;;;-4 first test for text";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_12()
        {
            text test = new text();
            string to_test = "ub;;;-3l;;;-4irst test for text";
            string to_test_prev = ";;;-3ub;;;-4irst test for ;;;-3text;;;-4";
            string expected = ";;;-3ubl;;;-4irst test for ;;;-3text;;;-4";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_13()
        {
            text test = new text();
            string to_test = ";;;-3ub;;;-4m first test for text";
            string to_test_prev = "lll;;;-3m;;;-4 first test for text";
            string expected = ";;;-3ubm;;;-4 first test for text";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
    }
}
