using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace ECount.Infrustructure.Data.Integration
{
    public enum RuleCommandType
    {
        Sql,
    }

    public class RuleCommandParameter
    {
        public string Name;
        public string Text;
        public object Value;
    }

    public interface IRuleCommand
    {
        RuleCommandType CommandType { get; }
        string Name { get; set; }
        IList<RuleCommandParameter> Parameters { get; set; }
        string CommandText { get; set; }
        bool Execute(ValidationResults validationResults);
    }

    public class SqlRuleCommand : IRuleCommand
    {
        private string _name;
        private string _commandText;
        private string _connectionName;
        private string _message;
        private IList<RuleCommandParameter> _parameters = new List<RuleCommandParameter>();
        public SqlRuleCommand(string name, string commandText, string connectionName, string message)
        {
            _name = name;
            _commandText = commandText;
            _connectionName = connectionName;
            _message = message;
        }

        #region IRuleCommand Members

        public RuleCommandType CommandType
        {
            get
            {
                return RuleCommandType.Sql;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public IList<RuleCommandParameter> Parameters
        {
            get
            {
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }

        public string CommandText
        {
            get
            {
                return _commandText;
            }
            set
            {
                _commandText = value;
            }
        }

        public bool Execute(ValidationResults validationResults)
        {
            try
            {
                string connectionstring;
                if (!string.IsNullOrEmpty(_connectionName))
                    connectionstring = ConfigurationManager.ConnectionStrings[_connectionName].ConnectionString;
                else
                    // the first one is LocalServer defined in machine.config
                    connectionstring = ConfigurationManager.ConnectionStrings[1].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    foreach (RuleCommandParameter param in _parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + param.Name.TrimStart('@'), param.Value));
                    }
                    // TODO: specialized commandtype, text or proc, here always treated as text
                    cmd.CommandText = CommandText;
                    connection.Open();

                    int result = (int)cmd.ExecuteScalar();

                    if (result == 0)
                    {
                        validationResults.AddResult(new ValidationResult(_message, Name));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(this, ex);
                validationResults.AddResult(new ValidationResult("验证失败, 详细信息:" + ex.Message + ".", Name));
                return false;
            }
            return true;
        }

        #endregion
    }

}
