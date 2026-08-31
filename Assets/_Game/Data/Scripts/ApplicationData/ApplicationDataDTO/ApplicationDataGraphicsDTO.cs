namespace Data.ApplicationData.DTO
{
    [System.Serializable]
    public class ApplicationDataGraphicsDTO
    {
        public int resolutionIndex;
        public int qualityLevel;
        public bool fullScreen;
        public int frameRate;
        public ApplicationDataGraphicsDTO(int resolutionIndex, int qualityLevel, bool fullScreen, int frameRate)
        {
            this.resolutionIndex = resolutionIndex;
            this.qualityLevel = qualityLevel;
            this.fullScreen = fullScreen;
            this.frameRate = frameRate;
        }
        public ApplicationDataGraphicsDTO()
        {
            resolutionIndex = 100;
            qualityLevel = 3;
            frameRate = -1;
        }
    }
}