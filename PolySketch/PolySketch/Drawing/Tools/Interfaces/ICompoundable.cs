namespace PolySketch.Drawing.Tools.Interfaces
{
    public interface ICompoundable
    {
        void StartNextPart();

        void Finish();
    }
}