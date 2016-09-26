using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Reflection;
using System.Data.Common;
using System.Linq.Expressions;
using System.Data.Metadata.Edm;
using System.Data.EntityClient;
using Microsoft.Data.Extensions;

namespace SGM.ECount.DataModel
{
    public static class ECountContextExtention
    {

        public static Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(
  Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            if (null == valueSelector) { throw new ArgumentNullException("valueSelector"); }
            if (null == values) { throw new ArgumentNullException("values"); }
            ParameterExpression p = valueSelector.Parameters.Single();
            if (!values.Any())
            {
                return e => false;
            }

            var equals = values.Select(value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));
            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        public static IQueryable<TEntity> WhereIn<TEntity, TValue>
         (
             this ObjectQuery<TEntity> query,
             Expression<Func<TEntity, TValue>> selector,
             IEnumerable<TValue> collection
         )
        {
            if (selector == null) throw new ArgumentNullException("selector");
            if (collection == null) throw new ArgumentNullException("collection");
            ParameterExpression p = selector.Parameters.Single();

            if (!collection.Any()) return query;

            IEnumerable<Expression> equals = collection.Select(value =>
               (Expression)Expression.Equal(selector.Body,
                    Expression.Constant(value, typeof(TValue))));

            Expression body = equals.Aggregate((accumulate, equal) =>
                Expression.Or(accumulate, equal));

            return query.Where(Expression.Lambda<Func<TEntity, bool>>(body, p));
        }
        public static void AttachUpdated(this ObjectContext context, EntityObject objectDetached)
        {
            //if (objectDetached.EntityState == EntityState.Detached)
            if (objectDetached.EntityState == EntityState.Detached || objectDetached.EntityState == EntityState.Modified)//modified by chenyh            
            {
                object currentEntityInDb = null;
                if (context.TryGetObjectByKey(objectDetached.EntityKey, out currentEntityInDb))
                {
                    context.ApplyPropertyChanges(objectDetached.EntityKey.EntitySetName, objectDetached);
                    //Apply property changes to all referenced entities in context 
                    context.ApplyReferencePropertyChanges((IEntityWithRelationships)objectDetached,
           (IEntityWithRelationships)currentEntityInDb);
                }
                else
                {
                    throw new ObjectNotFoundException();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="newEntity"></param>
        /// <param name="oldEntity"></param>
        public static void ApplyReferencePropertyChanges(this ObjectContext context, IEntityWithRelationships newEntity, IEntityWithRelationships oldEntity)
        {
            foreach (var relatedEnd in oldEntity.RelationshipManager.GetAllRelatedEnds())
            {
                var oldRef = relatedEnd as EntityReference;
                if (oldRef != null)
                {
                    // this related end is a reference not a collection 
                    var newRef = newEntity.RelationshipManager.GetRelatedEnd(oldRef.RelationshipName, oldRef.TargetRoleName) as EntityReference;

                    EntityObject refEntity = newRef.GetType().GetProperty("Value").GetValue(newRef, null) as EntityObject;
                    if (newRef.EntityKey == null && refEntity != null)
                    {
                        newRef.EntityKey = context.GetEntityKey(refEntity);
                    }

                    oldRef.EntityKey = newRef.EntityKey;
                }
            }
        }


        /// <summary>
        /// attache entity as unchanged
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <param name="context">object context</param>
        /// <param name="entity">entity to be attached</param>
        /// <returns>attached entity</returns>
        public static T AttachExistedEntity<T>(this ObjectContext context, T entity)
        where T : EntityObject
        {
            EdmEntityTypeAttribute entityTypeAttr = (EdmEntityTypeAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(EdmEntityTypeAttribute), false);
            if (entityTypeAttr == null)
                throw new NotSupportedException("T is not an entity.");
            string entityFullname = context.DefaultContainerName + "." + entityTypeAttr.Name;
            entity.EntityKey = new System.Data.EntityKey(entityFullname,
            from p in typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
            where p.GetGetMethod(false) != null
            let attribute = (EdmScalarPropertyAttribute)Attribute.GetCustomAttribute(p, typeof(EdmScalarPropertyAttribute))
            where attribute != null && attribute.EntityKeyProperty
            select new KeyValuePair<string, object>(p.Name, p.GetValue(entity, null)));
            context.Attach(entity);
            context.ApplyPropertyChanges(entityTypeAttr.Name, entity);
            return entity;
        }

        public static EntityObject AttachExistedObject(this ObjectContext context, object objEntity)
        {
            EntityObject entity = objEntity as EntityObject;
            EdmEntityTypeAttribute entityTypeAttr = (EdmEntityTypeAttribute)Attribute.GetCustomAttribute(objEntity.GetType(), typeof(EdmEntityTypeAttribute), false);
            if (entityTypeAttr == null)
                throw new NotSupportedException("T is not an entity.");
            if (entity.EntityKey == null)
            {
                string entityFullname = context.DefaultContainerName + "." + entityTypeAttr.Name;
                entity.EntityKey = new System.Data.EntityKey(entityFullname,
                from p in objEntity.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                where p.GetGetMethod(false) != null
                let attribute = (EdmScalarPropertyAttribute)Attribute.GetCustomAttribute(p, typeof(EdmScalarPropertyAttribute))
                where attribute != null && attribute.EntityKeyProperty
                select new KeyValuePair<string, object>(p.Name, p.GetValue(entity, null)));
            }
            context.Attach(entity);
            context.ApplyPropertyChanges(entityTypeAttr.Name, entity);
            return entity;
        }

        public static void AttachReferenceProperties<T>(this ObjectContext context, T entity)
        where T : EntityObject
        {
            PropertyInfo[] props = entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.FlattenHierarchy);
            Dictionary<PropertyInfo, EntityObject> refEntityList = new Dictionary<PropertyInfo, EntityObject>();
            foreach (var item in props)
            {

                if (item.PropertyType.BaseType == typeof(EntityObject))
                {
                    var refEntity = item.GetValue(entity, null) as EntityObject;
                    item.SetValue(entity, null, null);
                    refEntityList.Add(item, refEntity);
                }
            }
            foreach (var item in refEntityList.Keys)
            {
                EntityObject refEntity = refEntityList[item];

                if (refEntity!=null&&refEntity.EntityKey != null && refEntity.EntityState != EntityState.Unchanged && refEntity.EntityState != EntityState.Detached)
                {
                    context.Detach(refEntity);
                }
                //refEntity.EntityKey == null ||
                if (refEntity != null && refEntity.EntityState == EntityState.Detached)
                {
                    refEntity = AttachExistedObject(context, refEntity);
                }
                item.SetValue(entity, refEntity, null);
            }

        }

        public static void ApplyPropertyChanges<T>(this ObjectContext context, T entity)
            where T : EntityObject
        {
            EdmEntityTypeAttribute entityTypeAttr = (EdmEntityTypeAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(EdmEntityTypeAttribute), false);
            if (entityTypeAttr == null)
                throw new NotSupportedException("T is not an entity.");
            context.ApplyPropertyChanges(entityTypeAttr.Name, entity);
        }

        /// <summary>
        /// set the state of an attached entity to changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="context">attached entity to be changed</param>
        public static void SetAllModified<T>(this T entity, ObjectContext context)
where T : IEntityWithKey
        {
            var stateEntry = context.ObjectStateManager.GetObjectStateEntry(entity.EntityKey);

            var propertyNameList = stateEntry.CurrentValues.DataRecordInfo.FieldMetadata.Select(pn => pn.FieldType.Name);

            foreach (var propName in propertyNameList)
            {
                stateEntry.SetModifiedProperty(propName);
            }
        }


        public static EntityCollection<TElement> CreateCollection<T, TElement>(this T entity, Expression<Func<T, EntityCollection<TElement>>> expr, params TElement[] items)
            where T : EntityObject
            where TElement : EntityObject
        {
            if (expr.Body.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentException("Expression is not correct.", "expr");
            var member = ((MemberExpression)expr.Body).Member;
            PropertyInfo pi = member as PropertyInfo;
            if (pi == null)
                throw new ArgumentException("Expression is not correct.", "expr");
            EdmRelationshipNavigationPropertyAttribute attribute = (EdmRelationshipNavigationPropertyAttribute)Attribute.GetCustomAttribute(pi, typeof(EdmRelationshipNavigationPropertyAttribute));
            EntityCollection<TElement> result = new EntityCollection<TElement>();
            RelationshipManager rm = RelationshipManager.Create(entity);
            rm.InitializeRelatedCollection(attribute.RelationshipName, attribute.TargetRoleName, result);
            foreach (var item in items)
                result.Add(item);
            return result;
        }

        public static IEnumerable<EntitySet> GetEntitySets<T>(this ObjectContext ctx)
        {

            var objectItemCollection = ctx.MetadataWorkspace.GetItemCollection(DataSpace.OSpace) as ObjectItemCollection;



            //var entityType = (from oType in objectItemCollection.GetItems<EntityType>()

            //                  where objectItemCollection.GetClrType(oType) == typeof(T)

            //                  select ctx.MetadataWorkspace.GetEdmSpaceType(oType)).First();



            var entitySets = from container in ctx.MetadataWorkspace.GetItems<EntityContainer>(DataSpace.CSpace)

                             from set in container.BaseEntitySets.OfType<EntitySet>()

                             where set.ElementType.GetType().Equals(typeof(T))

                             select set;



            return entitySets.AsEnumerable();

        }

        public static EntityKey GetEntityKey<T>(this ObjectContext context, T entity)
        where T : EntityObject
        {
            EdmEntityTypeAttribute entityTypeAttr = (EdmEntityTypeAttribute)Attribute.GetCustomAttribute(entity.GetType(), typeof(EdmEntityTypeAttribute), false);
            if (entityTypeAttr == null)
                throw new NotSupportedException("T is not an entity.");
            string entityFullname = context.DefaultContainerName + "." + entityTypeAttr.Name;
            return new System.Data.EntityKey(entityFullname,
            from p in entity.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
            where p.GetGetMethod(false) != null
            let attribute = (EdmScalarPropertyAttribute)Attribute.GetCustomAttribute(p, typeof(EdmScalarPropertyAttribute))
            where attribute != null && attribute.EntityKeyProperty
            select new KeyValuePair<string, object>(p.Name, p.GetValue(entity, null)));
        }


        #region ad hoc sql helper

        public static DataTable LoadDataTable(this ObjectContext context, string sqlString, CommandType commandType, params DbParameter[] parameters)
        {
            return context.LoadDataTable(sqlString, commandType, null, parameters);
        }

        public static DataTable LoadDataTable(this ObjectContext context, string sqlString, CommandType commandType, DbTransaction transaction, params DbParameter[] parameters)
        {
            DbCommand cmd = context.PrepareCommand(sqlString, commandType, transaction, parameters);
            using (context.Connection.CreateConnectionScope())
            {
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }

        public static object ExecuteScalar(this ObjectContext context, string sqlString, CommandType commandType, params DbParameter[] parameters)
        {
            return context.ExecuteScalar(sqlString, commandType, null, parameters);
        }

        public static object ExecuteScalar(this ObjectContext context, string sqlString, CommandType commandType, DbTransaction transaction, params DbParameter[] parameters)
        {
            using (context.Connection.CreateConnectionScope())
            {
                DbCommand cmd = context.PrepareCommand(sqlString, commandType, transaction, parameters);
                return cmd.ExecuteScalar();
            }
        }

        public static int ExecuteNonQuery(this ObjectContext context, string sqlString, CommandType commandType, params DbParameter[] parameters)
        {
            return context.ExecuteNonQuery(sqlString, commandType, null, false, parameters);
        }

        public static int ExecuteNonQuery(this ObjectContext context, string sqlString, CommandType commandType, bool requiredTrans, params DbParameter[] parameters)
        {
            return context.ExecuteNonQuery(sqlString, commandType, null, requiredTrans, parameters);
        }

        public static int ExecuteNonQuery(this ObjectContext context, string sqlString, CommandType commandType, DbTransaction transaction, bool requiredTrans, params DbParameter[] parameters)
        {
            DbCommand cmd = context.PrepareCommand(sqlString, commandType, transaction, parameters);
            DbTransaction newTransaction = null;
            using (context.Connection.CreateConnectionScope())
            {
                if (transaction == null && requiredTrans)
                {
                    newTransaction = cmd.Connection.BeginTransaction();
                }
                int result = -1;
                try
                {
                    result = cmd.ExecuteNonQuery();
                    if (newTransaction != null)
                    {
                        newTransaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (newTransaction != null)
                    {
                        newTransaction.Rollback();
                    }
                    throw ex;
                }
                return result;
            }
        }

        public static DbCommand PrepareCommand(this ObjectContext context, string sqlString, CommandType commandType, DbTransaction transaction, params DbParameter[] parameters)
        {
            //if (_dbFactory == null)
            //{
            //    var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            //    var factoryProperty = typeof(EntityConnection).GetProperty("StoreProviderFactory", bindingFlags);
            //    _dbFactory = factoryProperty.GetValue(_context.Connection, null) as DbProviderFactory;
            //    _connection = _dbFactory.CreateConnection();
            //    _connection.ConnectionString = (_context.Connection as EntityConnection).StoreConnection.ConnectionString;
            //}
            DbConnection connection = (context.Connection as EntityConnection).StoreConnection;

            DbCommand command = connection.CreateCommand();//_dbFactory.CreateCommand();
            command.Connection = connection;
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            command.CommandText = sqlString;
            command.CommandType = commandType;
            if (parameters.Length > 0)
            {
                command.Parameters.Clear();
                command.Parameters.AddRange(parameters);
            }
            command.CommandTimeout = 180;
            return command;
        }

        public static DbParameter CreateDbParameter(this ObjectContext context, string paramName, DbType dbType, object paramValue, ParameterDirection direction)
        {
            DbParameter parameter = (context.Connection as EntityConnection).StoreConnection.CreateCommand().CreateParameter();
            parameter.DbType = dbType;
            parameter.ParameterName = paramName;
            parameter.Value = (paramValue == null) ? DBNull.Value : paramValue;
            parameter.Direction = direction;

            return parameter;
        }

        public static DbTransaction BeginTransaction(this ObjectContext context)
        {
            DbConnection connection = ((EntityConnection)context.Connection).StoreConnection;
            //connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            return transaction;
        }
        #endregion

    }

    // Allows almost-automated re-use of connections across multiple call levels
    //  while still controlling connection lifetimes.  Multiple connections are supported within a single scope.
    // To use:
    //  Create a new connection scope object in a using statement at the level within which you 
    //      want to scope connections.
    //  Use Current.AddConnection() and Current.GetConnection() to store/retrieve specific connections based on your
    //      own keys.
    //  Simpler alternative: Use Current.GetOpenConnection(factory, connection string) where you need to use the connection
    //
    // Example of simple case:
    //  void TopLevel() {
    //      using (DbConnectionScope scope = new DbConnectionScope()) {
    //          // Code that eventually calls LowerLevel a couple of times.
    //          // The first time LowerLevel is called, it will allocate and open the connection
    //          // Subsequent calls will use the already-opened connection, INCLUDING running in the same 
    //          //   System.Transactions transaction without using DTC (assuming only one connection string)!
    //      }
    //  }
    //
    //  void LowerLevel() {
    //      string connectionString = <...get connection string from config or somewhere...>;
    //      SqlCommand cmd = new SqlCommand("Some TSQL code");
    //      cmd.Connection = (SqlConnection) DbConnectionScope.Current.GetOpenConnection(SqlClientFactory.Instance, connectionString);
    //      ... finish setting up command and execute it
    //  }

    /// <summary>
    /// Class to assist in managing connection lifetimes inside scopes on a particular thread.
    /// </summary>
    sealed public class DbConnectionScope : IDisposable
    {
        #region class fields
        [ThreadStatic()]
        private static DbConnectionScope __currentScope = null;      // Scope that is currently active on this thread
        private static Object __nullKey = new Object();   // used to allow null as a key
        #endregion

        #region instance fields
        private DbConnectionScope _priorScope;    // previous scope in stack of scopes on this thread
        private Dictionary<object, DbConnection> _connections;   // set of connections contained by this scope.
        #endregion

        #region public class methods and properties
        /// <summary>
        /// Obtain the currently active connection scope
        /// </summary>
        public static DbConnectionScope Current
        {
            get
            {
                return __currentScope;
            }
        }
        #endregion

        #region public instance methods and properties
        /// <summary>
        /// Constructor
        /// </summary>
        public DbConnectionScope()
        {
            // Devnote:  Order of initial assignment is important in cases of failure!
            //  _priorScope first makes sure we know who we need to restore
            //  _connections second, to make sure we no-op dispose until we're as close to
            //      correct setup as possible
            //  __currentScope last, to make sure the thread static only holds validly set up objects
            _priorScope = __currentScope;
            _connections = new Dictionary<object, DbConnection>();
            __currentScope = this;
        }

        /// <summary>
        /// Convenience constructor to add an initial connection
        /// </summary>
        /// <param name="key">Key to associate with connection</param>
        /// <param name="connection">Connection to add</param>
        public DbConnectionScope(object key, DbConnection connection)
            : this()
        {
            AddConnection(key, connection);
        }

        /// <summary>
        /// Add a connection and associate it with the given key
        /// </summary>
        /// <param name="key">Key to associate with the connection</param>
        /// <param name="connection">Connection to add</param>
        public void AddConnection(object key, DbConnection connection)
        {
            CheckDisposed();
            if (null == key)
            {
                key = __nullKey;
            }
            _connections[key] = connection;
        }

        /// <summary>
        /// Check to see if there is a connection associated with this key
        /// </summary>
        /// <param name="key">Key to use for lookup</param>
        /// <returns>true if there is a connection, false otherwise</returns>
        public bool ContainsKey(object key)
        {
            CheckDisposed();
            return _connections.ContainsKey(key);
        }

        /// <summary>
        /// Shut down this instance.  Disposes all connections it holds and restores the prior scope.
        /// </summary>
        public void Dispose()
        {
            if (!IsDisposed)
            {
                // Firstly, remove ourselves from the stack (but, only if we are the one on the stack)
                //  Note: Thread-local _currentScope, and requirement that scopes not be disposed on other threads
                //      means we can get away with not locking.
                if (__currentScope == this)
                {
                    // In case the user called dispose out of order, skip up the chain until we find
                    //  an undisposed scope.
                    DbConnectionScope prior = _priorScope;
                    while (null != prior && prior.IsDisposed)
                    {
                        prior = prior._priorScope;
                    }
                    __currentScope = prior;
                }

                // secondly, make sure our internal state is set to "Disposed"
                IDictionary<object, DbConnection> connections = _connections;
                _connections = null;

                // Lastly, clean up the connections we own
                foreach (DbConnection connection in connections.Values)
                {
                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Get the connection associated with this key. Throws if there is no entry for the key.
        /// </summary>
        /// <param name="key">Key to use for lookup</param>
        /// <returns>Associated connection</returns>
        public DbConnection GetConnection(object key)
        {
            CheckDisposed();

            // allow null-ref as key
            if (null == key)
            {
                key = __nullKey;
            }

            return _connections[key];
        }

        /// <summary>
        /// This method gets the connection using the connection string as a key.  If no connection is
        /// associated with the string, the connection factory is used to create the connection.
        /// Finally, if the resulting connection is in the closed state, it is opened.
        /// </summary>
        /// <param name="factory">Factory to use to create connection if it is not already present</param>
        /// <param name="connectionString">Connection string to use</param>
        /// <returns>Connection in open state</returns>
        public DbConnection GetOpenConnection(DbProviderFactory factory, string connectionString)
        {
            CheckDisposed();
            object key;

            // allow null-ref as key
            if (null == connectionString)
            {
                key = __nullKey;
            }
            else
            {
                key = connectionString;
            }

            // go get the connection
            DbConnection result;
            if (!_connections.TryGetValue(key, out result))
            {
                // didn't find it, so create it.
                result = factory.CreateConnection();
                result.ConnectionString = connectionString;
                _connections[key] = result;
            }

            // however we got it, open it if it's closed.
            //  note: don't open unless state is unambiguous that it's ok to open
            if (ConnectionState.Closed == result.State)
            {
                result.Open();
            }

            return result;
        }
        #endregion

        #region private methods and properties
        /// <summary>
        /// Was this instance previously disposed?
        /// </summary>
        private bool IsDisposed
        {
            get
            {
                return null == _connections;
            }
        }

        /// <summary>
        /// Handle calling API function after instance has been disposed
        /// </summary>
        private void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("DbConnectionScope");
            }
        }

        #endregion
    }

}
