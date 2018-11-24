namespace OrlovMikhail.LJ.Grabber.Postprocess.Userpics
{
    public interface IUserpicStorageFactory
    {
        IUserpicStorage CreateOn(string basePath);
    }
}
