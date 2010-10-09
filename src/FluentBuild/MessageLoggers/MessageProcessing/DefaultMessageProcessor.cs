﻿using System;
using System.Collections.Generic;
using FluentBuild.Core;
using ConsoleColor = FluentBuild.Utilities.ConsoleColor;

namespace FluentBuild.MessageLoggers.MessageProcessing
{
    //used by Execetable to examine output and error strings and process accordingly
    internal class DefaultMessageProcessor : IMessageProcessor
    {
        public void Display(string prefix, string output, string error, int exitCode)
        {
            IList<Message> lines = Parse(prefix, output, error, exitCode);
            foreach (Message message in lines)
            {
                switch (message.MessageType)
                {
                    case MessageType.Regular:
                        MessageLogger.Write(message.Prefix, message.Contents);
                        break;
                    case MessageType.Warning:
                        MessageLogger.WriteWarning(message.Prefix, message.Contents);
                        break;
                    case MessageType.Error:
                        MessageLogger.WriteError(message.Prefix, message.Contents);
                        break;
                    default:
                        throw new NotImplementedException("Message type has not been implemented. Type: " +
                                                          message.MessageType);
                }
            }
        }

        public IList<Message> Parse(string prefix, string output, string error, int processeExitCode)
        {
            var messages = new List<Message>();

            MessageType defaultMessageType = MessageType.Regular;

            if (processeExitCode != 0)
                defaultMessageType = MessageType.Error;

            foreach (string line in output.Split(Environment.NewLine.ToCharArray()))
            {
                if (line.Trim().Length > 0)
                {
                    var message = new Message(prefix, defaultMessageType);
                    if (line.Contains("warning") || line.Contains("Warning"))
                        message = new Message(prefix, MessageType.Warning);
                    message.Contents = line;
                    messages.Add(message);
                }
            }

            foreach (string line in error.Split(Environment.NewLine.ToCharArray()))
            {
                if (line.Trim().Length > 0)
                {
                    var message = new Message(prefix, MessageType.Error);
                    message.Contents = line;
                    messages.Add(message);
                }
            }

            return messages;
        }
    }
}