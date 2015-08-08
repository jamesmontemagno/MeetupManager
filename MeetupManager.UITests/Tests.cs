using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MeetupManager.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;

        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("App Launched.");

        }

        [Test]
        public void LoginSuccess()
        {
            app.Screenshot("First screen.");
            Login();
        }

        [Test]
        public void LoginFailure()
        {
            app.Screenshot("First screen.");

            Login(password: "ThisIsWrong", gotoGroups: false);
           
            app.Tap(x => x.Marked("Cancel"));
            app.Query(x => x.Marked("Login Failure"));
            app.Tap(x => x.Marked("OK"));
            app.WaitForElement(x => x.Marked("ButtonLogin"));

        }


         

        [Test]
        public void CheckInMember()
        {
            app.Screenshot("First Screen");
            Login();

            app.Query(x => x.Marked("GroupsList"));
            app.WaitForElement(x => x.Marked("Name"));
            app.Tap(x => x.Marked("Name"));
            app.Screenshot("Selected Group");
            app.WaitForElement(x => x.Marked("Name"));
            app.Tap(x => x.Marked("Name"));
            app.Screenshot("Selected Event");
            app.WaitForElement(x => x.Marked("CheckedIn"));
            app.Screenshot("Pre-checkin");
            app.Tap(x => x.Marked("CheckedIn"));
            var checks = app.Query(x => x.Marked("CheckedIn"));
            Assert.IsTrue(checks[0].Enabled);
            app.Screenshot("Checked In");
        }

        [Test]
        public void CheckOutMember()
        {
            app.Screenshot("First Screen");
            Login();

            app.Query(x => x.Marked("GroupsList"));
            app.WaitForElement(x => x.Marked("Name"));
            app.Tap(x => x.Marked("Name"));
            app.Screenshot("Selected Group");
            app.WaitForElement(x => x.Marked("Name"));
            app.Tap(x => x.Marked("Name"));
            app.Screenshot("Selected Event");
            app.WaitForElement(x => x.Marked("CheckedIn"));
            app.Screenshot("Pre-checkin");
            app.Tap(x => x.Marked("CheckedIn"));
            var checks = app.Query(x => x.Marked("CheckedIn"));
            Assert.IsTrue(checks[0].Enabled);
            app.Screenshot("Checked In");
            app.Tap(x => x.Marked("CheckedIn"));
            checks = app.Query(x => x.Marked("CheckedIn"));
            Assert.IsFalse(checks[0].Enabled);
            app.Screenshot("Checked Out");
        }

        [Test]
        public void AddNewMember()
        {
            app.Screenshot("First Screen");
            Login();

            app.Query(x => x.Marked("GroupsList"));
            app.Tap(x => x.Marked("Name"));
            app.Screenshot("Selected Group");
            app.WaitForElement(x => x.Marked("Name"));
            app.Tap(x => x.Marked("Name"));
            app.Screenshot("Selected Event");
            app.WaitForElement(x => x.Marked("Name"));
            app.Repl();
            app.Tap("AddNewPerson");
            app.WaitForElement("OK");
            app.EnterText("James");
            app.Tap("OK");
            app.WaitForElement("OK");
            var result = app.Query("James you are all set!");
            Assert.IsNotNull(result);
            app.Tap("OK");
            app.ScrollDownTo("James");
            result = app.Query("James");
            Assert.IsNotNull(result);
        }

        [Test]
        public void RemoveNewMember()
        {
            AddNewMember();
            var result = app.Query("James")[0];
            app.DragCoordinates(result.Rect.X, result.Rect.Y, 0, result.Rect.Y);
            app.Tap("Remove");
            app.Tap("OK");
            var result2 = app.Query("James");
            Assert.IsNull(result2);
        }


        private void Login(string username ="ENTERaEMAIL", string password ="THISISNOTAPASS", bool gotoGroups = true)
        {
            app.Tap(x => x.Marked("ButtonLogin"));
            app.WaitForElement(x => x.Css("#login-view"));

            app.Screenshot("Tapped Login");

            app.ScrollDown();
            app.Screenshot("Scroll to login button.");

            app.EnterText(x => x.Css("#login-email"), username);
            app.Screenshot("Enter Username.");

            app.EnterText(x => x.Css("#login-password"), password);
            app.Screenshot("Enter Password.");
            app.Tap(x => x.Css("#login-button"));
            if (gotoGroups)
            {
                app.WaitForElement(x => x.Marked("BusyStack"));
                app.Screenshot("Logged In.");
                app.WaitForElement(x => x.Marked("Groups"));//navigated to groups
                app.Screenshot("Groups Visible.");
            }
        }
    }
}

