﻿using Domain.Models;

namespace Web.Models {
    public class AccountView {
        public IAccount Data { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}