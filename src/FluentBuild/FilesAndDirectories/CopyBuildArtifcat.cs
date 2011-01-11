using System;
using System.IO;
using FluentBuild.Core;
using FluentBuild.Tokenization;
using FluentBuild.Utilities;

namespace FluentBuild.FilesAndDirectories
{
    ///<summary>
    /// Copies a build artifact
    ///</summary>
    public class CopyBuildArtifcat : Failable<CopyBuildArtifcat>
    {
        private readonly IFileSystemWrapper _fileSystemWrapper;
        private readonly BuildArtifact source;

        internal CopyBuildArtifcat(IFileSystemWrapper fileSystemWrapper, BuildArtifact artifact)
        {
            _fileSystemWrapper = fileSystemWrapper;
            source = artifact;
        }

        internal CopyBuildArtifcat(BuildArtifact artifact) : this(new FileSystemWrapper(), artifact)
        {
        }

        ///<summary>
        /// Destination
        ///</summary>
        ///<param name="artifactDestination">The destination</param>
        public void To(BuildArtifact artifactDestination)
        {
            To(artifactDestination.ToString());
        }

        ///<summary>
        /// Destination
        ///</summary>
        ///<param name="folderDestination">The destination</param>
        public void To(BuildFolder folderDestination)
        {
            To(folderDestination.ToString());
        }


        ///<summary>
        /// Destination
        ///</summary>
        ///<param name="destination">The destination path</param>
        public void To(String destination)
        {
            string destinationFileName;
            string destinationDirectory;
            //if no filename in destination then get it from the source
            
            if (!Path.HasExtension(destination))
            {
                destinationFileName = Path.GetFileName(source.ToString());
                destinationDirectory = destination;
            }
            else
            {
                destinationFileName = Path.GetFileName(destination);
                destinationDirectory = Path.GetDirectoryName(destination);
            }

// ReSharper disable AssignNullToNotNullAttribute
            string dest = Path.Combine(destinationDirectory, destinationFileName);
// ReSharper restore AssignNullToNotNullAttribute
            MessageLogger.WriteDebugMessage("Copy from " + source + " to " + dest);

            FailableActionExecutor.DoAction(base.OnError, _fileSystemWrapper.Copy, source.ToString(), dest);
            
        }


        /// <summary>
        /// Replaces a token in a file 
        /// </summary>
        /// <param name="token">the token to be replaced</param>
        /// <returns></returns>
        /// <example>tokens in the file are surrounded by @ signs. So ReplaceToken("name") would replace everything in a file with @name@</example>
        public TokenWith ReplaceToken(string token)
        {
            return new TokenReplacer(_fileSystemWrapper.ReadAllText(source.ToString())).ReplaceToken(token);
        }
    }
}