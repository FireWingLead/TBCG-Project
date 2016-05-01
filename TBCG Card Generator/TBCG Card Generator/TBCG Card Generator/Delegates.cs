namespace TBCG_Card_Generator
{
    delegate void ParentUpdater<T_Child, T_Parent>(T_Child updateChild, T_Parent withParent);
}