namespace Data.ApplicationData.DTO
{
    [System.Serializable]
    public class ApplicationDataDTO
    {
        public ApplicationDataGraphicsDTO graphics;
        //TODO Input
        //TODO Sound
        public ApplicationDataDTO()
        {
            graphics = new ApplicationDataGraphicsDTO();
        }
    }
}