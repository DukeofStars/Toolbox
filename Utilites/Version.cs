namespace Toolbox2.Update
{
    public sealed class Version
    {
        int _major = 0;
        int _minor = 0;
        int _patch = 0;
        public override string ToString()
        {
            return _major + "." + _minor + "." + _patch;
        }

        public override bool Equals(object obj)
        {
            return this == (obj as Version);
        }

        public override int GetHashCode()
        {
            int hashCode = -632995282;
            hashCode = hashCode * -1521134295 + _major.GetHashCode();
            hashCode = hashCode * -1521134295 + _minor.GetHashCode();
            hashCode = hashCode * -1521134295 + _patch.GetHashCode();
            return hashCode;
        }

        // Operators
        public static bool operator >(Version left, Version right)
        {
            if (left._major > right._major) return true;
            else if (left._minor > right._minor) return true;
            else if (left._patch > right._patch) return true;
            return false;
        }

        public static bool operator <(Version left, Version right)
        {
            if (left._major > right._major) return true;
            else if (left._minor > right._minor) return true;
            else if (left._patch > right._patch) return true;
            return false;
        }

        public static bool operator >=(Version left, Version right)
        { 
            return left > right || left == right;
        }

        public static bool operator <=(Version left, Version right)
        {
            return left < right || left == right;
        }

        public static bool operator ==(Version left, Version right)
        {
            return left._major == right._major && left._minor == right._minor && left._patch == right._patch;
        }

        public static bool operator !=(Version left, Version right)
        {
            return !(left == right);
        }
    }
}
