using System;

namespace au.util.comctl {
	public class PathMismatchException : ApplicationException {
		internal PathMismatchException(string basePath, string filename) : base(string.Format(Properties.Resources.PathMismatchExceptionMessage, basePath, filename)) { }
	}
	public class FolderNotFoundException : ApplicationException {
		internal FolderNotFoundException(string path) : base(string.Format(Properties.Resources.FolderNotFoundExceptionMessage, path)) { }
	}
}