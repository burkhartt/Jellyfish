using System;

namespace Entities {
    public class NullAccount : Account {
        public override bool IsNull() {
            return true;
        }
    }
}