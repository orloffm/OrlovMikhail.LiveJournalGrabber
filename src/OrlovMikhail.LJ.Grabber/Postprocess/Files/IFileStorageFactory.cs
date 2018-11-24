namespace OrlovMikhail.LJ.Grabber.PostProcess.Files
{
    /// <summary>Creates a file storage.</summary>
    public interface IFileStorageFactory
    {
        /// <summary>Directory that holds original data.</summary>
        IFileStorage CreateOn(string dumpLocation);
    }
}
