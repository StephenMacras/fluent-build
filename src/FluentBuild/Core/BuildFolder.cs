using System.IO;
using FluentBuild.FilesAndDirectories;
using FluentBuild.FilesAndDirectories.FileSet;
using FluentBuild.Utilities;

namespace FluentBuild.Core
{

    /// <summary>
    /// Represents a folder used in the build process.
    /// </summary>
    public class BuildFolder
    {
        private readonly IFileSystemWrapper _fileSystemWrapper;
        private readonly IFileSetFactory _fileSetFactory;
        private readonly string _path;

        internal BuildFolder(IFileSystemWrapper fileSystemWrapper, IFileSetFactory fileSetFactory, string path)
        {
            _fileSystemWrapper = fileSystemWrapper;
            _fileSetFactory = fileSetFactory;
            _path = path;
        }

        ///<summary>
        /// Creates a new BuildFolder that is used by the build or will be created during the build
        ///</summary>
        ///<param name="path">Path to the folder</param>
        public BuildFolder(string path) : this(new FileSystemWrapper(), new FileSetFactory(),  path)
        {            
        }

        /// <summary>
        /// Creates a new BuildFolder object for a subdirectory
        /// </summary>
        /// <param name="path">The subfolder below the current BuildFolder</param>
        /// <returns>a new BuildFolder object</returns>
        /// <remarks>The folder does not need to exist to use this method</remarks>
        public BuildFolder SubFolder(string path)
        {
            return new BuildFolder(Path.Combine(_path, path));
        }


        /// <summary>
        /// Creates a new BuildFolder that encompases the current folder and all of its subdirectories
        /// </summary>
        /// <returns>A buildfolder that represents the current folder and all its subdirectories</returns>
        public BuildFolder RecurseAllSubFolders()
        {
            return new BuildFolder(_path + "\\**\\");
        }


        /// <summary>
        /// Deletes the folder. If the the folder can not be deleted, or does not exist then an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public BuildFolder Delete()
        {
            return Delete(Defaults.OnError);
        }

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        ///<param name="onError">Sets the behavior of how to handle an error</param>
        ///<returns></returns>
        public BuildFolder Delete(OnError onError)
        {
            MessageLogger.WriteDebugMessage("Deleting " + _path);
            FailableActionExecutor.DoAction(onError, _fileSystemWrapper.DeleteDirectory, _path, true);
            return this;
        }

        ///<summary>
        /// Creates the folder
        ///</summary>
        ///<returns></returns>
        public BuildFolder Create()
        {
            return Create(Defaults.OnError);
        }
       
        ///<summary>
        /// Creates the folder
        ///</summary>
        ///<param name="onError">Allows you to set the error behavior</param>
        ///<returns></returns>
        public BuildFolder Create(OnError onError)
        {
            MessageLogger.WriteDebugMessage("Create Direcotry " + _path);
            FailableActionExecutor.DoAction(onError, _fileSystemWrapper.CreateDirectory, _path);
            return this;
        }

        /// <summary>
        /// Provides the current path internal to the BuildFolder object
        /// </summary>
        /// <returns>The path of the BuildFolder</returns>
        public override string ToString()
        {
            return _path;
        }

        /// <summary>
        /// Appends a filename onto the BuildFolder
        /// </summary>
        /// <param name="name">The name (or filter) of the file</param>
        /// <returns>A BuildArtifact that represents the full path to the file</returns>
        public BuildArtifact File(string name)
        {
            return new BuildArtifact(Path.Combine(_path, name));
        }

        ///<summary>
        /// Creates a fileset based on a filter
        ///</summary>
        ///<param name="filter">A filter that can contain wildcards</param>
        ///<returns></returns>
        public FileSet Files(string filter)
        {
            var fileSet = _fileSetFactory.Create();
            return fileSet.Include(Path.Combine(_path, filter));
        }
    }
}