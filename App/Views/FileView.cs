﻿namespace App.Views
{
    using MyWebServer.Server.Views.Contracts;

    public class FileView : IView
    {
        private readonly string htmlFile;

        public FileView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View() => this.htmlFile;
    }
}