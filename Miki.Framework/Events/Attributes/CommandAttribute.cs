﻿using Miki.Common;
using System;

namespace Miki.Framework.Events.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        internal CommandEvent command = new CommandEvent();
        internal string on = "";

        public EventAccessibility Accessibility
        {
            get => command.Accessibility;
            set => command.Accessibility = value;
        }

        public string[] Aliases
		{
            get => command.Aliases;
            set => command.Aliases = value;
        }

        public string Name
        {
            get => command.Name;
            set => command.Name = value.ToLower();
        }

        public string On
        {
            get => on;
            set => on = value.ToLower();
        }

        public bool CanBeDisabled
        {
            get => command.CanBeDisabled;
            set => command.CanBeDisabled = value;
        }

        public CommandAttribute()
        {
        }
    }
}