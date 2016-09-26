using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data.Objects.DataClasses;
using System.Data;
using System.Data.Objects;
using System.Data.Common;
using System.Data.EntityClient;
using System.Reflection;
using Microsoft.Data.Extensions;
using System.Runtime.Serialization;
using System.IO;

namespace SGM.ECount.BLL
{
    public class BaseGenericBLL<T>
        where T : EntityObject
    {
        protected ECountContext _context;
        private string _entitySetName;
        //private DbConnection _connection;
        //private DbProviderFactory _dbFactory;
        public ECountContext Context
        {
            get
            {
                return _context;
            }
        }

        public BaseGenericBLL(string entitySetName)
        {
            _context = new ECountContext();
            _context.CommandTimeout = 300;
            _entitySetName = _context.DefaultContainerName + "." + entitySetName;
        }

        public BaseGenericBLL(ECountContext context, string entitySetName)
        {
            this._context = context;
            _entitySetName = _context.DefaultContainerName + "." + entitySetName;
        }

        #region object CRUD

        protected T GetObjectByKey(T entity)
        {
            return this.GetObjectByKey(entity, false);
        }

        protected T GetObjectByKey(T entity, bool lazyLoad)
        {
            EntityKey key = _context.CreateEntityKey(_entitySetName, entity);
            object result;
            if (_context.TryGetObjectByKey(key, out result))
            {
                if (!lazyLoad)
                {
                    RelationshipManager relation = ((IEntityWithRelationships)result).RelationshipManager;
                    foreach (var relatedEnd in relation.GetAllRelatedEnds())
                    {
                        var relatedRef = relatedEnd as EntityReference;
                        if (relatedRef != null && !relatedRef.IsLoaded)
                        {
                            relatedRef.Load();
                        }
                    }
                }
                return result as T;
            }
            return null;
        }

        protected ObjectQuery<T> GetObjects()
        {
            return _context.CreateQuery<T>(_entitySetName);//.ToList();
        }


        public List<T> GetList()
        {
            ObjectQuery<T> objQry = this.GetObjects();
            if (objQry != null)
            {
                return objQry.ToList();
            }
            else
            {
                return new List<T>();
            }
        }

        protected void UpdateObject(T entity)
        {
            UpdateObject(entity, true);
        }

        protected void UpdateObject(T entity, bool saveChanges)
        {
            try
            {
                if (entity.EntityKey == null)
                {
                    entity.EntityKey = _context.CreateEntityKey(_entitySetName, entity);
                }
                _context.AttachUpdated(entity);

                if (saveChanges)
                {
                    _context.SaveChanges();
                }
            }
            catch (OptimisticConcurrencyException opEx)
            {
                _context.Refresh(RefreshMode.ClientWins, entity);
                if (saveChanges)
                {
                    _context.SaveChanges();
                }
                throw opEx;
            }
        }

        protected IQueryable<E> GetQueryByPage<E>(IQueryable<E> query, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            itemCount = query.Count();

            pageCount = (itemCount - 1) / pageSize + 1;
            int rowSkip = pageSize * (pageNumber - 1);
            if (pageNumber > pageCount)
            {
                rowSkip = (pageCount - 1) * pageNumber;
            }
            return query.Skip(rowSkip).Take(pageSize);
        }

        /// <summary>
        /// mark entity to be deleted and save it
        /// </summary>
        /// <param name="entity">entity to delete</param>
        protected void DeleteObject(T entity)
        {
            this.DeleteObject(entity, true);
        }

        /// <summary>
        /// mark entity to be deleted 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        protected void DeleteObject(T entity, bool saveChanges)
        {
            _context.AttachExistedEntity(entity);
            _context.DeleteObject(entity);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        protected T AddObject(T entity)
        {
            return AddObject(entity, true);
        }

        protected T AddObject(T entity, bool saveChanges)
        {
            _context.AttachReferenceProperties(entity);
            _context.AddObject(_entitySetName, entity);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
            return entity;
        }

        #endregion

        #region utilities

        protected static F DataContractSerialization<F>(F obj)
        {
            DataContractSerializer dcSer = new DataContractSerializer(obj.GetType());
            MemoryStream memoryStream = new MemoryStream();

            dcSer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;

            F newObject = (F)dcSer.ReadObject(memoryStream);
            return newObject;
        }

        public string FormatIds(List<string> ids)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in ids)
            {
                sb.Append("'" + str + "',");
            }
            if (sb.Length > 0)
                return sb.Remove(sb.Length - 1, 1).ToString();
            else
                return "''";

        }
        #endregion


    }

}
