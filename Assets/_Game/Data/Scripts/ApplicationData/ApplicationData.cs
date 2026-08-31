namespace Data.ApplicationData
{
    public interface IApplicationData
    {
        public IApplicationDataGraphicsReadonly GetGraphicsReadonly { get; }
        public IApplicationDataGraphicsSetter GetGraphicsSetter { get; }
    }
    public class ApplicationData : IApplicationData
    {
        private readonly DTO.ApplicationDataDTO _applicationDataDTO;
        private readonly ApplicationDataGraphics _graphics;
        public DTO.ApplicationDataDTO GetDTO => _applicationDataDTO;//need to set link on data for saving when data don't be get from init
        public IApplicationDataGraphicsReadonly GetGraphicsReadonly => _graphics;
        public IApplicationDataGraphicsSetter GetGraphicsSetter => _graphics;
        public ApplicationData(DTO.ApplicationDataDTO applicationDataDTO = null)
        {
            _applicationDataDTO = applicationDataDTO;
            _applicationDataDTO ??= new();
            _graphics = new ApplicationDataGraphics(_applicationDataDTO.graphics);
        }
    }
}