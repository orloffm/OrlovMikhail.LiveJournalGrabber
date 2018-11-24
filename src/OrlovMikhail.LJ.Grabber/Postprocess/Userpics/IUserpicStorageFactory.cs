namespace OrlovMikhail.LJ.Grabber.PostProcess.Userpics
{
    public interface IUserpicStorageFactory
    {
        IUserpicStorage CreateOn(string basePath);
    }
}
