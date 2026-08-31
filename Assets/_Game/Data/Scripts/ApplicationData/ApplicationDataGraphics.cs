namespace Data.ApplicationData
{
    //interfaces only for closing class
    public interface IApplicationDataGraphicsReadonly
    {
        public event System.Action EventOnChanged;
        public bool IsFullScreen { get; }
        public int ResolutionIndex { get; }
        public int QualityIndex { get; }
        public int FrameRate { get; }
    }
    public interface IApplicationDataGraphicsSetter : IApplicationDataGraphicsReadonly
    {
        public void Update(bool isFullScreen, int resolutionIndex, int qualityIndex, int frameRate);
    }
    public class ApplicationDataGraphics : IApplicationDataGraphicsSetter
    {
        public event System.Action EventOnChanged;
        private readonly DTO.ApplicationDataGraphicsDTO _dto;
        public ApplicationDataGraphics(DTO.ApplicationDataGraphicsDTO dto = null) {
            _dto = dto;
            _dto ??= new();
        }
        public bool IsFullScreen => _dto.fullScreen;
        public int ResolutionIndex => _dto.resolutionIndex;
        public int QualityIndex => _dto.qualityLevel;
        public int FrameRate => _dto.frameRate;
        public void Update(bool isFullScreen, int resolutionIndex, int qualityIndex, int frameRate)
        {
            _dto.frameRate = frameRate;
            _dto.resolutionIndex = resolutionIndex;
            _dto.qualityLevel = qualityIndex;
            _dto.fullScreen = isFullScreen;
            EventOnChanged?.Invoke();
        }
    }
}