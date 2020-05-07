using DataAccess;
using System;

namespace Logic
{
    public class BaseLogic : IDisposable
    {
        /// <summary>
        /// 执行操作,同时判断是否需要保存
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="needSave"></param>
        public void save(Action action, bool needSave = true)
        {
            action();
            if (needSave)
            {
                work.Save();
            }

        }
        public UnitOfWork work = new UnitOfWork();

        /// <summary>
        /// 标记Dispose()方法是否被调用过
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// 保存更改
        /// </summary>
        public virtual void Save()
        {
            if (work != null)
            {
                work.Save();
            }
        }
        /// <summary>
        /// 清理资源的虚方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // 清理托管资源
                    work.Dispose();
                }
                else
                {
                    //清理非托管资源
                }
            }
            this.disposed = true;
        }

        #region IDisposable interface

        public void Dispose()
        {
            Dispose(true);
            //阻止GC调用Finalize方法
            GC.SuppressFinalize(this);
        }

        #endregion
    }
    
}

