public static class GameProgress
{
    public static bool introDialogueFinished = false;
    public static bool evidenceScene1Finished = false;
    public static bool evidenceScene2Finished = false;
    public static bool returnedToMap1 = false;
      public static bool part1EvidenceComplete = false;
    public static bool soulDialogueComplete = false;
     public static bool guestDialogueComplete = false;
    public static bool assistantDialogueComplete = false;
    public static bool supporterDialogueComplete = false;


    public static bool CanGoToNextMap()
    {
        return part1EvidenceComplete && soulDialogueComplete;
    }
    public static bool CanReturnToMap1()
    {
        return guestDialogueComplete &&
               assistantDialogueComplete &&
               supporterDialogueComplete;
    }
}
