using System;
using Xamarin.Forms;
using MeetupManager.Portable.Helpers;

namespace MeetupManager.Portable.Views
{

    public class AboutView : ContentPage
    {
        
        SwitchCell showAll, organizerMode, feedback;
        public AboutView ()
        {
            Title = "Settings";
            var color = Color.FromHex("738182");

            Content = new TableView {
                Intent = TableIntent.Form,
                Root = new TableRoot (Title) {
                    (new TableSection ("Preferences") {
                        (organizerMode = new SwitchCell {
                            Text = "Show only groups I organize",
                            On = Settings.OrganizerMode,
                            StyleId="OragnizerMode"
                        }),
                        (showAll = new SwitchCell {
                            Text = "Show all events",
                            On = Settings.ShowAllEvents,
                            StyleId = "ShowAll"
                        }),
                        (feedback = new SwitchCell {
                            Text = "Automated Feedback",
                            On = Settings.Insights,
                            StyleId = "Feedback"
                        })
                    }),
                    new TableSection ("About Meetup Manager") {
                        new TextCell {
                            Text = "Created by",
                            TextColor= color,
                            Detail = "James Montemagno",
                            Command = GoToSiteCommandExt,
                            CommandParameter = "http://m.twitter.com/jamesmontemagno"
                        },
                        new TextCell {
                            Text = "Version 2.0",
                            TextColor= color,
                            Detail = "Copyright 2015 Refractored LLC",
                            Command = GoToSiteCommandExt,
                            CommandParameter = "http://refractored.com"
                        },
                        new TextCell {
                            Text = "Created with",
                            TextColor= color,
                            Detail = "C# and Xamarin",
                            Command = GoToSiteCommandExt,
                            CommandParameter = "http://xamarin.com"
                        },
                        new TextCell {
                            Text = "Support Forum",
                            TextColor= color,
                            Command = GoToSiteCommandExt,
                            CommandParameter = "https://plus.google.com/u/1/communities/111605797436259290945"
                        }
                    },
                    new TableSection ("Technology Use") {
                        new TextCell {
                            Text = "Xamarin.Forms",
                            TextColor= color,
                            Detail ="By: Xamarin",
                            Command = GoToSiteCommandExt,
                            CommandParameter = "http://xamarin.com/forms"
                        },
                        new TextCell {
                            Text = "Modern HttpClient",
                            TextColor= color,
                            Detail ="By: Paul Betts",
                            Command = GoToSiteCommandExt,
                            CommandParameter = "https://components.xamarin.com/view/modernhttpclient"
                        },
                        new TextCell {
                            Text = "Json.NET",
                            TextColor= color,
                            Detail ="By: James Newton-King",
                            Command = GoToSiteCommandExt,
                            CommandParameter = "https://components.xamarin.com/view/json.net"
                        },
                        new TextCell {
                            Text = "Settings Plugin for Xamarin",
                            TextColor= color,
                            Detail ="By: James Montemagno",
                            Command = GoToSiteCommandExt,
                            CommandParameter = "https://github.com/jamesmontemagno/Xamarin.Plugins/tree/master/Settings"
                        },
                        new TextCell {
                            Text = "Image Circle Plugin",
                            TextColor= color,
                            Detail ="By: James Montemagno",
                            Command = GoToSiteCommandExt,
                            CommandParameter = "https://github.com/jamesmontemagno/Xamarin.Plugins/tree/master/ImageCircle"
                        }, new TextCell {
                            Text = "SQLite-net PCL",
                            TextColor= color,
                            Detail ="By: Frank Krueger",
                            Command = GoToSiteCommandExt,
                            CommandParameter = "https://github.com/praeclarum/sqlite-net"
                        },
                    },
                }
            };                  
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Settings.Insights = feedback.On;
            Settings.ShowAllEvents = showAll.On;
            Settings.OrganizerMode = organizerMode.On;
        }
           

        private Command<string> goToSiteCommandExt;
        public Command<string> GoToSiteCommandExt
        {
            get{
                return goToSiteCommandExt ?? (goToSiteCommandExt = new Command<string> (ExecuteGoToSiteExtCommand));
            }
        }

        void ExecuteGoToSiteExtCommand (string site)
        {
            Device.OpenUri(new Uri(site));
        }
    }
}

