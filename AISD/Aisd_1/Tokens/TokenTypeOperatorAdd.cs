namespace Aisd_1.Tokens;

public class TokenTypeOperatorAdd : TokenTypeOperator
{
    public override string StringRepresentation => "+";
    
    public override OperatorKind OperatorKind => OperatorKind.Binary | OperatorKind.Unary;
}