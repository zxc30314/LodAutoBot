using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LodAutoBot;
using static Data;
using static LodAutoBot.ClickImage;
using static LodAutoBot.Form1.Other;
using static LodAutoBot.State;

namespace LodAutoBot
{
    public enum State
    {
        未知,
        地圖,
        地區,
        探索完畢,
        發現怪物,
        選擇同伴,
        戰鬥中,
        結算介面,
        捕捉到怪物,
        製造所,
        發現地下城,
        封鎖地下城,
        地下城封鎖完畢,
        找地下城,
        別人領地,
        地下城目錄,
        地下城發現獎勵,
        收益報告,
        EventPage,
        地下城,
        探索報告,
        怪物統一問候結果,
        怪物立即捕捉結果
    }
}

class StateControl
{
    public async Task Setup(CancellationToken cts)
    {
        while (!cts.IsCancellationRequested)
        {
            await new UnKnow().Setup(cts);
        }
    }
}

abstract class Data
{
    public static Dictionary<string, ImageData> windowsData = new();
    public static Dictionary<string, ImageData> clickData = new();
    public static Dictionary<string, ImageData> monsterData = new();
    public static Dictionary<string, ImageData> underGroundData = new();
    public static Dictionary<string, ImageData> intimacyData = new();
    public static Dictionary<string, ImageData> otherData = new();
    public static Dictionary<Level, Form1.CheckBoxControll> expeditionUnderGround = new();
    public static Dictionary<Level, Form1.CheckBoxControll> catchMonsters = new();
    public static readonly DmControll dm = new();
    public static bool expeditionNotFind;
    public static int UnderGroundState;
    public static bool hasJourney;
    public static bool hasCapture;
    public static bool useReport;

    public static readonly Dictionary<LodAutoBot.State, State> list = new()
    {
        { 未知, new UnKnow() },
        { LodAutoBot.State.地圖, new Map() },
        { 地區, new Area() },
        { 探索完畢, new Complete() },
        { 發現怪物, new MonsterDiscovered() },
        { 選擇同伴, new Conquer() },
        { 戰鬥中, new OnWar() },
        { LodAutoBot.State.結算介面, new PerfectWin() },
        { 捕捉到怪物, new CapturedTheMonster() },
        { 製造所, new Storage() },
        { 發現地下城, new DiscoveredDungeon() },
        { 封鎖地下城, new CloseDungeon() },
        { LodAutoBot.State.地下城封鎖完畢, new DiscoveredDungeonEnd() },
        { 找地下城, new FindDungeon() },
        { 別人領地, new OtherArea() },
        { 地下城目錄, new DungeonList() },
        { 地下城, new Dungeon() },
        { 地下城發現獎勵, new EndDungeon() },
        { 收益報告, new Report() },
        { 探索報告, new ExplorationReport() },
        { 怪物統一問候結果, new GreetAllResults() },
        { 怪物立即捕捉結果, new InstantCaptureResults() }
    };
}

internal class InstantCaptureResults : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 怪物立即捕捉結果))
        {
            ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);
        dm.FindAllPictureAndClick(clickData, 確認);
    }
}

internal class GreetAllResults : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 怪物統一問候結果))
        {
            ChangeState(未知, cts);
            return;
        }

        await Task.Delay(1000, cts);
        dm.FindAllPictureAndClick(clickData, 立即捕捉);
        await Task.Delay(1000, cts);
        dm.FindAllPictureAndClick(windowsData, 怪物統一問候結果);
        dm.FindAllPictureAndClick(clickData, 確認);
    }
}

internal class ExplorationReport : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 探索報告))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(1000, cts);
        if (!dm.FindAllPictureAndClick(clickData, 放棄所有地下城))
        {
            return;
        }

        await Task.Delay(1000, cts);
        if (!dm.FindAllPicture(otherData, 發現的怪物0))
        {
            dm.FindAllPictureAndClick(clickData, 統一問候);
        }
        else
        {
            await Task.Delay(1000, cts);
            dm.FindAllPictureAndClick(clickData, 確認);
        }
    }
}

public abstract class State
{
    public async Task RestWindows(Dictionary<string, ImageData> datas, CancellationToken cts)
    {
        for (int i = 1; i < datas.Count; i++)
        {
            for (int j = 0; j < datas.ElementAt(i).Value.Paths.Length; j++)
            {
                if (dm.FindPicture(datas.ElementAt(i).Value.Paths[j], 0.7, datas.ElementAt(i).Value.Rectangle[j]).isFind)
                {
                    await ChangeState((LodAutoBot.State)i, cts);
                }
            }
        }
    }

    public abstract Task Setup(CancellationToken cts);


    protected async Task ChangeState(LodAutoBot.State state, CancellationToken cts)
    {
        await list[state].Setup(cts);
        Trace.WriteLine(state);
    }
}

internal class DiscoveredDungeonEnd : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPictureAndClick(windowsData, LodAutoBot.State.地下城封鎖完畢))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);
    }
}

internal class FindDungeon : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 找地下城))
        {
            await ChangeState(未知, cts);
            return;
        }

        expeditionNotFind = false;
        await Task.Delay(100, cts);
        dm.FindAllPictureAndClick(clickData, 開始遠征);
    }
}

internal class CloseDungeon : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 封鎖地下城))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);


        switch (UnderGroundState)
        {
            case 0: // 封鎖
                dm.FindAllPictureAndClick(clickData, 強制封鎖);
                break;
            case 1: //協商
                dm.FindAllPictureAndClick(clickData, 協商封鎖);
                break;

            default:
                dm.FindAllPictureAndClick(clickData, 關閉);
                break;
        }
    }
}

internal class Report : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 收益報告))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);
        dm.FindAllPictureAndClick(clickData, 確認);
    }
}

internal class EndDungeon : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPictureAndClick(windowsData, 地下城發現獎勵))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(1000, cts);
        expeditionNotFind = false;
    }
}

internal class Dungeon : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 地下城))
        {
            await ChangeState(未知, cts);
            return;
        }

        expeditionNotFind = false;
        await Task.Delay(500);
        dm.FindAllPictureAndClick(clickData, 攻略);
    }
}

internal class DungeonList : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 地下城目錄))
        {
            await ChangeState(未知, cts);
            return;
        }

        for (int i = Enum.GetNames(typeof(Level)).Length - 1; i >= 0; i--)
        {
            if (expeditionUnderGround[(Level)i].Checked && dm.FindAllPictureAndClick(underGroundData, (Level)i, new Rectangle(127, 259, 635, 876)))
            {
                expeditionNotFind = false;
                await Task.Delay(100, cts);
                dm.FindAllPictureAndClick(clickData, 前往地下城);
                break;
            }

            expeditionNotFind = true;
        }

        if (expeditionNotFind)
        {
            dm.FindAllPictureAndClick(clickData, 關閉);
        }
    }
}

internal class OtherArea : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 別人領地))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);
        if (expeditionNotFind)
        {
            dm.FindAllPictureAndClick(clickData, 探索其他領地);
            expeditionNotFind = false;
        }
        else
        {
            dm.FindAllPictureAndClick(clickData, 地下城資訊);
        }
    }
}

internal class Storage : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 製造所))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100);
        dm.FindAllPictureAndClick(clickData, 關閉);
    }
}

internal class CapturedTheMonster : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 捕捉到怪物))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);
        dm.FindAllPictureAndClick(clickData, 確認);
    }
}

internal class PerfectWin : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, LodAutoBot.State.結算介面))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);
        dm.FindAllPictureAndClick(clickData, ClickImage.結算介面);
    }
}

internal class OnWar : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 戰鬥中))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);
        dm.FindAllPictureAndClick(clickData, Skip);
    }
}

internal class Conquer : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 選擇同伴))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);
        dm.FindAllPictureAndClick(clickData, 戰鬥開始);
    }
}

internal class MonsterDiscovered : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 發現怪物))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);

        if (!hasCapture)
        {
            dm.FindAllPictureAndClick(clickData, 問候); //問候
            return;
        }


        for (int i = 0; i < Enum.GetNames(typeof(Level)).Length; i++)
        {
            if (catchMonsters[(Level)i].Checked && dm.FindAllPicture(monsterData, (Level)i))
            {
                dm.FindAllPictureAndClick(clickData, 嘗試捕捉); //捕捉
                return;
            }
        }

        dm.FindAllPictureAndClick(clickData, 問候); //問候
    }
}

internal class DiscoveredDungeon : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 發現地下城))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);
        switch (UnderGroundState)
        {
            case 0: // 封鎖
                if (!dm.FindAllPictureAndClick(clickData, 立即封鎖))
                {
                    dm.FindAllPictureAndClick(clickData, 強制封鎖);
                }

                break;
            case 1: //協商
                if (!dm.FindAllPictureAndClick(clickData, 立即封鎖))
                {
                    dm.FindAllPictureAndClick(clickData, 關閉);
                }

                break;
            case 2: //佔領
                if (!dm.FindAllPictureAndClick(clickData, 戰鬥開始))
                {
                    dm.FindAllPictureAndClick(clickData, 關閉);
                }

                break;
        }
    }
}

internal class Complete : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, 探索完畢))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);
        dm.FindAllPictureAndClick(clickData, 再一次);
    }
}

internal class UnKnow : State
{
    public override async Task Setup(CancellationToken cts)
    {
        await RestWindows(windowsData, cts);
    }
}

class Map : State
{
    public override async Task Setup(CancellationToken cts)
    {
        if (!dm.FindAllPicture(windowsData, LodAutoBot.State.地圖))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(500, cts);

        if (dm.FindAllPictureAndClick(clickData, 村收, 0.5f))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(500, cts);

        if (!dm.FindAllPicture(clickData, 探索報告_小) && hasJourney)
        {
            dm.FindAllPictureAndClick(clickData, 遠征, 0.5f);
        }
        else if (dm.FindAllPicture(clickData, 探索報告_小) && useReport)
        {
            dm.FindAllPictureAndClick(clickData, 探索報告_小);
        }
        else if (dm.FindAllPicture(clickData, 探索報告_小) && !useReport)
        {
            dm.FindAllPictureAndClick(clickData, ClickImage.地圖);
        }
    }
}

class Area : State
{
    public override async Task Setup(CancellationToken cts)
    {
        await Task.Delay(100, cts);
        if (!dm.FindAllPicture(windowsData, 地區, 0.8d))
        {
            await ChangeState(未知, cts);
            return;
        }

        await Task.Delay(100, cts);

        if (!await FindAndClickComplete(cts))
        {
            dm.FindAllPictureAndClick(clickData, 關閉);
        }

        await Task.Delay(300, cts);
    }

    async Task<bool> FindAndClickComplete(CancellationToken cancellationToken)
    {
        bool isFind = false;
        int[] x = new int[] { 1, -1, 0, 1, 0 };
        int[] y = new int[] { 1, 0, -1, -0, 1 };
        int[] count = new int[] { 2, 4, 3, 4, 3 };
        for (int i = 0; i < x.Length; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (await DoMouseDropLoop(x[i], y[i], count[i], cancellationToken))
            {
                await Task.Delay(300, cancellationToken);
                dm.FindAllPictureAndClick(clickData, 探索完畢_藍);
                return true;
            }

            await Task.Delay(300, cancellationToken);
        }

        return false;
    }

    async Task<bool> DoMouseDropLoop(int tx, int ty, int tcount, CancellationToken cancellationToken)
    {
        for (int i = 0; i < tcount; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (dm.FindAllPictureAndClick(clickData, 探索完畢_白))
            {
                return true;
            }

            await Task.Delay(300, cancellationToken);
            await MouseDrop(tx, ty, 30, cancellationToken);
            await Task.Delay(300, cancellationToken);
        }

        return false;
    }

    async Task MouseDrop(int x, int y, int count, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        dm.MoveTo(700, 500);
        dm.LeftDown();
        for (int i = 0; i < count; i++)
        {
            dm.MoveR(i * x, i * y);
            await Task.Delay(20, cancellationToken);
        }

        await Task.Delay(50, cancellationToken);
        dm.LeftUp();
    }
}