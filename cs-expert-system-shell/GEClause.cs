using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chen0040.ExpertSystem
{
    public class GEClause : Clause
    {
        public GEClause(string variable, string value)
            : base(variable, value)
	    {
		    Condition=">=";
	    }
	
	    protected override  IntersectionType Intersect(Clause rhs)
	    {
		    string v1=_value;
		    string v2=rhs.Value;
		
		    var a=0.0;
		    var b=0.0;

            if(double.TryParse(v1, out a) && double.TryParse(v2, out b))
            {
		        if(rhs is LessClause)
		        {
			        //v1 >= a
			        //v2 < b 
			        //mutually exclusive: b <= a
			        //unmatched: b > a
			        if(b <= a)
			        {
				        return IntersectionType.MUTUALLY_EXCLUDE;
			        }
			        else
			        {
				        return IntersectionType.UNKNOWN;
			        }
		        }
		        else if(rhs is LEClause)
		        {
			        //v1 >= a
			        //v2 < b 
			        //matched: b <= a
			        //unmatched: b > a
			        if(b <= a)
			        {
				        return IntersectionType.MUTUALLY_EXCLUDE;
			        }
			        else
			        {
				        return IntersectionType.UNKNOWN;
			        }
		        }
		        else if(rhs is IsClause)
		        {
			        //v1 >= a
			        //v2 = b
			        //matched: b >= a
			        //mutually exclusive: b < a
			        if(b >= a)
			        {
				        return IntersectionType.INCLUDE;
			        }
			        else
			        {
				        return IntersectionType.MUTUALLY_EXCLUDE;
			        }
		        }
		        else if(rhs is GEClause)
		        {
			        //v1 >= a
			        //v2 >= b
			        //mutually exclusive: b >= a
			        //unmatched: b < a
			        if(b >= a)
			        {
				        return IntersectionType.INCLUDE;
			        }
			        else
			        {
				        return IntersectionType.UNKNOWN;
			        }
		        }
		        else if(rhs is GreaterClause)
		        {
			        //v1 >= a
			        //v2 > b
			        //mutually exclusive: b >= a
			        //unmatched: b < a
			        if(b >= a)
			        {
				        return IntersectionType.INCLUDE;
			        }
			        else
			        {
				        return IntersectionType.UNKNOWN;
			        }
		        }
		        else
		        {
			        return IntersectionType.UNKNOWN;
		        }
            }
            else
            {
                return IntersectionType.UNKNOWN;
            }
	    }
    }
}
