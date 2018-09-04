namespace Infrastructure.Factory
{
    public class FileFactory
    {
        #region 单例模式
        private static readonly FileFactory instance = new FileFactory();
        private FileFactory() { }
        public static FileFactory GetInstance => instance;
        #endregion

        public static string GetBuildPath
        {//数据库实体生成
            get
            {
                var filePath = string.Empty;
                var paths = System.IO.Directory.GetCurrentDirectory().Split('\\');
                filePath = paths[0] + "\\" + paths[1] + "\\" + paths[2];
                return filePath + Utility.Config.CurrentBuildModelPath;
            }
        }
    }
}
