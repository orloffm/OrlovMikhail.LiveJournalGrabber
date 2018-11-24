using OrlovMikhail.LJ.Grabber.Entities;
using OrlovMikhail.LJ.Grabber.Extractor.FolderNamingStrategy;

namespace OrlovMikhail.LJ.Grabber.Extractor.Interfaces
{
    /// <summary>Orchestrates everything, downloads stuff to folders.</summary>
    public interface IWorker
    {
        EntryPage Work(string URI, string rootLocation, IFolderNamingStrategy subFolderGetter, string cookie);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="URI"></param>
        /// <param name="rootLocation"></param>
        /// <param name="innerFolder">Folder inside year folder.</param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        EntryPage WorkInGivenTarget(string URI, string rootLocation, string innerFolder, string cookie);
    }
}
