using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Common.Constants
{
    public static class ErrorMessages
    {
        public static readonly string NotFound = "not_found";
        public static readonly string FailedLogin = "failed_login";

        public static readonly string ActionInProgress = "action_in_progress";
        public static readonly string ActionRequestTooLong = "action_request_too_long";

        public static readonly string NotEnoughResources = "not_enough_resources";
        public static readonly string CannotAttackWithNoAtk = "cannot_attack_with_no_atk";

        public static readonly string NotEnoughPlayers = "not_enough_players";
    }
}
