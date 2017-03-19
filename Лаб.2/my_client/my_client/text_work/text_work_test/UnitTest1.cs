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
        [TestMethod]
        public void add_changings_14()
        {
            text test = new text();
            string to_test = "my first test ;;;-3tr;;;-4text";
            string to_test_prev = ";;;-3my ;;;-4first test for text";
            string expected = ";;;-3my ;;;-4first test ;;;-3tr;;;-4text";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_15()
        {
            text test = new text();
            string to_test = ";;;-3;;;-4first test for text";
            string to_test_prev = "my ;;;-3first;;;-4 test for;;;-3 text;;;-4";
            string expected = ";;;-3first;;;-4 test for;;;-3 text;;;-4";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_16()
        {
            text test = new text();
            string to_test = "my ;;;-3hgf;;;-4 test for text";
            string to_test_prev = ";;;-3my ;;;-4first test for text";
            string expected = ";;;-3my hgf;;;-4 test for text";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_17()
        {
            text test = new text();
            string to_test = "my first test for ;;;-3hgf;;;-4";
            string to_test_prev = ";;;-3my ;;;-4first test for text";
            string expected = ";;;-3my ;;;-4first test for ;;;-3hgf;;;-4";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_18()
        {
            text test = new text();
            string to_test = "my fir;;;-3hgf;;;-4 test for text";
            string to_test_prev = ";;;-3my ;;;-4first test ;;;-3for;;;-4 text";
            string expected = ";;;-3my ;;;-4fir;;;-3hgf;;;-4 test ;;;-3for;;;-4 text";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_19()
        {
            text test = new text();
            string to_test = "testing my client\nsomething\nand something;;;-3s;;;-4 elsegf";
            string to_test_prev = "testing my client\nsomething\nand something else;;;-3gf;;;-4";
            string expected = "testing my client\nsomething\nand something;;;-3s;;;-4 else;;;-3gf;;;-4";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_20()
        {
            text test = new text();
            string to_test = "testing my client\nsomething\nand somethings;;;-3om;;;-4 elsegf";
            string to_test_prev = "testing my client\nsomething\nand something;;;-3s;;;-4 else;;;-3gf;;;-4";
            string expected = "testing my client\nsomething\nand something;;;-3som;;;-4 else;;;-3gf;;;-4";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void add_changings_21()
        {
            text test = new text();
            string to_test = "testing my client\nsomething\nand something;;;-3s;;;-4 elsegf";
            string to_test_prev = ";;;-3te;;;-4sting my client\nsomething\nand something elsegf";
            string expected = ";;;-3te;;;-4sting my client\nsomething\nand something;;;-3s;;;-4 elsegf";
            string result = test.adding_to_other_changings(to_test, to_test_prev);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]

        public void find_all_changes_1()
        {
            text test = new text();
            text.changed[] mas_expected = new text.changed[10];
            text.changed[] mas_result = new text.changed[10];
            string cur = "my new test for finding all changes in text";
            string change = ";;;-3my;;;-4 ;;;-3old;;;-4 test ;;;-3;;;-4finding all;;;-3the;;;-4 changes in text;;;-3file;;;-4";
            mas_expected[0].beg = 0; mas_expected[0].changed_part = "my"; mas_expected[0].end = 2;
            mas_expected[1].beg = 3; mas_expected[1].changed_part = "old"; mas_expected[1].end = 6;
            mas_expected[2].beg = 12; mas_expected[2].changed_part = ""; mas_expected[2].end = 16;
            mas_expected[3].beg = 23; mas_expected[3].changed_part = "the"; mas_expected[3].end = 23;
            mas_expected[4].beg = 42; mas_expected[4].changed_part = "file"; mas_expected[4].end = 42;
            int n1 = test.find_all_changes(cur, change, ref mas_result);
            CollectionAssert.AreEqual(mas_expected, mas_result);
        }
        [TestMethod]

        public void find_all_changes_2()
        {
            text test = new text();
            text.changed[] mas_expected = new text.changed[10];
            text.changed[] mas_result = new text.changed[10];
            string cur = "my new test for finding all changes in textfil";
            string change = ";;;-3my;;;-4 ;;;-3old;;;-4 test ;;;-3f;;;-4finding all;;;-3the;;;-4 changes in text;;;-3file;;;-4";
            mas_expected[0].beg = 0; mas_expected[0].changed_part = "my"; mas_expected[0].end = 2;
            mas_expected[1].beg = 3; mas_expected[1].changed_part = "old"; mas_expected[1].end = 6;
            mas_expected[2].beg = 12; mas_expected[2].changed_part = "f"; mas_expected[2].end = 16;
            mas_expected[3].beg = 24; mas_expected[3].changed_part = "the"; mas_expected[3].end = 24;
            mas_expected[4].beg = 43; mas_expected[4].changed_part = "file"; mas_expected[4].end = 46;
            int n1 = test.find_all_changes(cur, change, ref mas_result);
            CollectionAssert.AreEqual(mas_expected, mas_result);
        }

        [TestMethod]
        public void find_all_changes_3()
        {
            text test = new text();
            text.changed[] mas_expected = new text.changed[10];
            text.changed[] mas_result = new text.changed[10];
            string cur = "my new test for finding all changes in textfi";
            string change = ";;;-3my;;;-4 ;;;-3old;;;-4 test ;;;-3;;;-4finding all;;;-3the;;;-4 changes in text;;;-3file;;;-4";
            mas_expected[0].beg = 0; mas_expected[0].changed_part = "my"; mas_expected[0].end = 2;
            mas_expected[1].beg = 3; mas_expected[1].changed_part = "old"; mas_expected[1].end = 6;
            mas_expected[2].beg = 12; mas_expected[2].changed_part = ""; mas_expected[2].end = 16;
            mas_expected[3].beg = 23; mas_expected[3].changed_part = "the"; mas_expected[3].end = 23;
            mas_expected[4].beg = 42; mas_expected[4].changed_part = "file"; mas_expected[4].end = 44;
            int n1 = test.find_all_changes(cur, change, ref mas_result);
            CollectionAssert.AreEqual(mas_expected, mas_result);
        }
        [TestMethod]
        public void find_all_changes_4()
        {
            text test = new text();
            text.changed[] mas_expected = new text.changed[10];
            text.changed[] mas_result = new text.changed[10];
            string cur = "my new test for finding all changes in text";
            string change = ";;;-3my;;;-4 ;;;-3old;;;-4 test ;;;-3fo;;;-4finding all;;;-3the;;;-4 changes in ;;;-3file;;;-4";
            mas_expected[0].beg = 0; mas_expected[0].changed_part = "my"; mas_expected[0].end = 2;
            mas_expected[1].beg = 3; mas_expected[1].changed_part = "old"; mas_expected[1].end = 6;
            mas_expected[2].beg = 12; mas_expected[2].changed_part = "fo"; mas_expected[2].end = 16;
            mas_expected[3].beg = 25; mas_expected[3].changed_part = "the"; mas_expected[3].end = 25;
            mas_expected[4].beg = 40; mas_expected[4].changed_part = "file"; mas_expected[4].end = 44;
            int n1 = test.find_all_changes(cur, change, ref mas_result);
            CollectionAssert.AreEqual(mas_expected, mas_result);
        }
        [TestMethod]
        public void find_all_changes_5()
        {
            text test = new text();
            text.changed[] mas_expected = new text.changed[10];
            text.changed[] mas_result = new text.changed[10];
            string cur ="testing my client\nsomething\nand something elsegf";
            string change = "testing my client\nsomething\nand something;;;-3s;;;-4 else;;;-3gf;;;-4";
            mas_expected[0].beg = 41; mas_expected[0].changed_part = "s"; mas_expected[0].end = 41;
            mas_expected[1].beg = 47; mas_expected[1].changed_part = "gf"; mas_expected[1].end = 49;
            int n1 = test.find_all_changes(cur, change, ref mas_result);
            CollectionAssert.AreEqual(mas_expected, mas_result);
        }
        [TestMethod]
        public void find_all_changes_6()
        {
            text test = new text();
            text.changed[] mas_expected = new text.changed[10];
            text.changed[] mas_result = new text.changed[10];
            string cur = "testing my client\nsomething\nand something elsegf";
            string change = "testing my client\nsomething\nand something;;;-3s;;;-4 elsegf";
            mas_expected[0].beg = 41; mas_expected[0].changed_part = "s"; mas_expected[0].end = 41;
            int n1 = test.find_all_changes(cur, change, ref mas_result);
            CollectionAssert.AreEqual(mas_expected, mas_result);
        }
        [TestMethod]
        public void find_all_changes_7()
        {
            text test = new text();
            text.changed[] mas_expected = new text.changed[10];
            text.changed[] mas_result = new text.changed[10];
            string cur = "my new test for finding all changes in text";
            string change = ";;;-3my;;;-4 ;;;-3old;;;-4 test ;;;-3;;;-4finding all;;;-3the;;;-4 changes in ;;;-3text;;;-4";
            mas_expected[0].beg = 0; mas_expected[0].changed_part = "my"; mas_expected[0].end = 2;
            mas_expected[1].beg = 3; mas_expected[1].changed_part = "old"; mas_expected[1].end = 6;
            mas_expected[2].beg = 12; mas_expected[2].changed_part = ""; mas_expected[2].end = 16;
            mas_expected[3].beg = 23; mas_expected[3].changed_part = "the"; mas_expected[3].end = 23;
            mas_expected[4].beg = 38; mas_expected[4].changed_part = "text"; mas_expected[4].end = 42;
            int n1 = test.find_all_changes(cur, change, ref mas_result);
            CollectionAssert.AreEqual(mas_expected, mas_result);
        }
        [TestMethod]
        public void find_all_changes_8()
        {
            text test = new text();
            text.changed[] mas_expected = new text.changed[10];
            text.changed[] mas_result = new text.changed[10];
            string cur = "testing my client\nsomething\nand something else";
            string change = "tes;;;-3i wa;;;-4ting my client\nsomething;;;-3ve;;;-4\nand something;;;-3donej;;;-4 else;;;-3go;;;-4";
            mas_expected[0].beg = 3; mas_expected[0].changed_part = "i wa"; mas_expected[0].end = 3;
            mas_expected[1].beg = 31; mas_expected[1].changed_part = "ve"; mas_expected[1].end = 31;
            mas_expected[2].beg = 47; mas_expected[2].changed_part = "donej"; mas_expected[2].end = 47;
            mas_expected[3].beg = 57; mas_expected[3].changed_part = "go"; mas_expected[3].end = 57;
            int n1 = test.find_all_changes(cur, change, ref mas_result);
            CollectionAssert.AreEqual(mas_expected, mas_result);
        }
        [TestMethod]
        public void find_all_changes_9()
        {
            text test = new text();
            text.changed[] mas_expected = new text.changed[10];
            text.changed[] mas_result = new text.changed[10];
            string cur = "testing my client\nsomething\nand something else";
            string change = ";;;-3;;;-4";
            mas_expected[0].beg = 0; mas_expected[0].changed_part = ""; mas_expected[0].end = 46;
            int n1 = test.find_all_changes(cur, change, ref mas_result);
            CollectionAssert.AreEqual(mas_expected, mas_result);
        }
    }
}
