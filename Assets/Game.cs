using GameCanvas;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// ゲームクラス。
/// 学生が編集すべきソースコードです。
/// </summary>
public sealed class Game : GameBase
{
    // 変数の宣言
    private int Stage;

    private int Day;

    private int LoveCount;
    private bool LoveFull;

    private const int NADERU = 1;
    private const int STUDY = 2;
    private const int TRAINING = 3;
    private const int SLAP = 4;
    private int MovePast;

    private int MoveCount;
    private int MoveNaderu;
    private int MoveStudy;
    private int MoveTraining;
    private int MoveSlap;
    private int MoveTea;
    private int MovePlay;
    private bool MoveOK;
    private bool MoveDV;

    //棒人間制御
    private bool NormalMan;
    private bool TrainingMan;
    private bool TrainingMan2;
    private bool StudyMan;
    private bool StudyMan2;
    private bool NaderuMan;
    private bool NaderuMan2;
    private bool TeaMan;
    private bool TeaMan2;
    private bool PlayMan;
    private bool PlayMan2;
    private bool SlapMan;

    //棒人間分岐
    private bool Yusha;
    private bool Neet;
    private bool Yankee;
    private bool Madam;
    private bool Doctor;
    private bool Macho;
    private bool Hitman;
    private bool People;
    private bool Girl;

    //棒人間分岐保存
    private bool YushaED;
    private bool NeetED;
    private bool YankeeED;
    private bool MadamED;
    private bool DoctorED;
    private bool MachoED;
    private bool HitmanED;
    private bool PeopleED;
    private bool GirlED;

    //棒人間回数
    private int YushaCount;
    private int NeetCount;
    private int YankeeCount;
    private int MadamCount;
    private int DoctorCount;
    private int MachoCount;
    private int HitmanCount;
    private int PeopleCount;
    private int GirlCount;

    //タイトルアニメ用
    private int Man_x;
    private bool Man1;
    private bool Man2;
    private bool Man3;
    private bool ManStop;

    //コンプリート判定
    private bool Complete;

    //SE制御
    private int SoundCount;

    //コマンドアニメ制御
    private int AnimationCount;
    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void InitGame()
    {
        // キャンバスの大きさを設定します
        gc.ChangeCanvasSize(720, 1280);

        MoveOK = true;
        NormalMan = true;
        Day = 1;
        //Stage = 16;
    }

    /// <summary>
    /// 動きなどの更新処理
    /// </summary>
    public override void UpdateGame()
    {
        //コンプリート判定
        if (PeopleED && NeetED && MachoED && YankeeED && DoctorED && YushaED && MadamED && YushaED && GirlED)
        {
            Complete = true;
        }

        //好感度MAX判定
        if (LoveCount == 3)
        {
            LoveFull = true;
        }
        else
        {
            LoveFull = false;
        }

        //ステージ処理
        if (Stage == 0)
        {
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 480 && 280 < gc.GetPointerX(0) && gc.GetPointerY(0) < 800 && 700 < gc.GetPointerY(0))
            {
                //始める
                Stage++;
                ManStop = true;
                gc.PlaySound(GcSound.Soundpiron, GcSoundTrack.SE, false);
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 450 && 300 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1100 && 1000 < gc.GetPointerY(0))
            {
                //図鑑
                Stage = 6;
                ManStop = true;
                gc.PlaySound(GcSound.Soundbook, GcSoundTrack.SE, false) ;
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 500 && 240 < gc.GetPointerX(0) && gc.GetPointerY(0) < 950 && 850 < gc.GetPointerY(0))
            {
                //遊び方
                Stage = 17;
                ManStop = true;
            }

            //アニメーション起動＆戻す
            if (Man_x == 0)
            {
                ManDraw();
            }
            else if (Man_x >= 720)
            {
                Man_x = 1;
            }

        }
        else if (Stage == 1)
        {
            if (gc.GetPointerFrameCount(0) == 1)
            {
                Stage++;
            }
        }
        else if (Stage == 2)
        {

            //なでるボタン
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 180 && 40 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1250 && 1150 < gc.GetPointerY(0) && MoveOK)
            {
                MoveDV = false;
                MoveNaderu++;
                NormalMan = false;
                MoveOK = false;
                NaderuManDraw();

                if ((MovePast == TRAINING || MovePast == STUDY) && LoveCount < 3)
                {
                    LoveCount++;
                }

                if (MovePast == SLAP)
                {
                    MoveDV = true;
                }

                MovePast = NADERU;
                gc.PlaySound(GcSound.Soundfu, GcSoundTrack.SE, false);
            }

            //筋トレボタン
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 350 && 210 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1250 && 1150 < gc.GetPointerY(0) && MoveOK)
            {
                MoveDV = false;
                NormalMan = false;
                MoveOK = false;
                TrainingManDraw();
                MoveTraining++;
                MovePast = TRAINING;
                gc.PlaySound(GcSound.Soundtraining, GcSoundTrack.SE, false);
            }

            //勉強ボタン
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 500 && 400 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1250 && 1150 < gc.GetPointerY(0) && MoveOK)
            {
                MoveDV = false;
                NormalMan = false;
                MoveOK = false;
                StudyManDraw();
                MoveStudy++;
                MovePast = STUDY;
                gc.PlaySound(GcSound.Soundpencil, GcSoundTrack.SE, false);
            }

            //ビンタボタン
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 660 && 560 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1250 && 1150 < gc.GetPointerY(0) && MoveOK)
            {
                if (MoveDV)
                {
                    LoveCount = 3;
                    MoveDV = false;
                }
                else if (LoveCount > 0)
                {
                    LoveCount--;
                }
                NormalMan = false;
                MoveOK = false;
                Invoke(nameof(NormalManDraw), 0.6f);
                SlapMan = true;              
                MoveSlap++;
                MovePast = SLAP;
                gc.SetSoundLevel(-20.0f, GcSoundTrack.SE);
                gc.PlaySound(GcSound.SoundSlap_low, GcSoundTrack.SE, false);
            }

            //お茶ボタン
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 690 && 590 < gc.GetPointerX(0) && gc.GetPointerY(0) < 450 && 350 < gc.GetPointerY(0) && MoveOK && LoveFull)
            {
                MoveDV = false;
                NormalMan = false;
                MoveOK = false;
                TeaManDraw();
                MoveTea++;
                MovePast = 0;
                gc.PlaySound(GcSound.Soundcup, GcSoundTrack.SE, false);
            }

            //遊ぶボタン
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 690 && 590 < gc.GetPointerX(0) && gc.GetPointerY(0) < 650 && 550 < gc.GetPointerY(0) && MoveOK && LoveFull)
            {
                MoveDV = false;
                NormalMan = false;
                MoveOK = false;
                PlayManDraw();
                MovePlay++;
                MovePast = 0;
                gc.PlaySound(GcSound.Soundwind, GcSoundTrack.SE, false);
            }

            //3回行動で次の日
            if (MoveCount == 3)
            {
                MoveOK = false;
                if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 440 && 300 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1200 && 1100 < gc.GetPointerY(0))
                {
                    MoveOK = true;
                    MoveCount++;
                    Day++;
                    gc.SetSoundLevel(-30.0f, GcSoundTrack.SE);
                    gc.PlaySound(GcSound.Soundteren, GcSoundTrack.SE, false);
                }
            }
            else if (MoveCount == 4 && Day == 4)
            {
                Stage = 4;
                MoveCount = 0;
            }
            else if (MoveCount == 4)
            {
                Stage = 3;
                MoveCount = 0;
                MoveOK = false;
            }
        }
        else if (Stage == 3)
        {
            MoveDV = false;
            MovePast = 0;
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 430 && 280 < gc.GetPointerX(0) && gc.GetPointerY(0) < 900 && 800 < gc.GetPointerY(0))
            {             
                Stage = 2;
                MoveOK = true;
            }
        }
        else if (Stage == 4)
        {
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 430 && 280 < gc.GetPointerX(0) && gc.GetPointerY(0) < 900 && 800 < gc.GetPointerY(0))
            {
                gc.PlaySound(GcSound.Soundjan, GcSoundTrack.SE, false);
                Stage = 5;
                //育成判定
                if (MoveNaderu == 3 && MoveTraining == 3 && MoveStudy == 3 && LoveCount >= 2 && MoveSlap == 0)
                {
                    //勇者
                    YushaCount++;
                    Yusha = true;
                }
                else if (MoveStudy <= 1 && MoveTraining >= 3  && MovePlay >= 1)
                {
                    //遊び人
                    YankeeCount++;
                    Yankee = true;
                }
                else if (MoveStudy <= 1 && MoveTraining <= 1 && LoveCount <= 1)
                {
                    //ニート
                    NeetCount++;
                    Neet = true;
                }
                else if (MoveStudy >= 2 && MoveTraining >= 5 && LoveCount == 0)
                {
                    //ヒットマン
                    HitmanCount++;
                    Hitman = true;
                }
                else if (MoveNaderu >= 1 && MoveStudy >= 6)
                {
                    //研究者
                    DoctorCount++;
                    Doctor = true;
                }
                else if (MoveStudy >= 2 && MoveTraining >= 2 && LoveCount == 3 && MoveTea >= 1)
                {
                    //マダム
                    MadamCount++;
                    Madam = true;
                }
                else if (MoveTraining >= 8)
                {
                    //マッチョ
                    MachoCount++;
                    Macho = true;
                }
                else if (LoveCount == 3 && MoveTea == 6)
                {
                    //美少女
                    GirlCount++;
                    Girl = true;
                }
                else
                {
                    //一般人
                    PeopleCount++;
                    People = true;
                }
            }
        }
        else if (Stage == 5)
        {
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 650 && 500 < gc.GetPointerX(0) && gc.GetPointerY(0) < 100 && 50 < gc.GetPointerY(0))
            {
                ManStop = false;
                Man_x = 0;
                Stage = 0;
                Reset();
            }
        }
        else if (Stage == 6)
        {
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 130 && 30 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1270 && 1170 < gc.GetPointerY(0))
            {
                ManStop = false;
                Man_x = 0;
                Stage = 0;
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 700 && 600 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1270 && 1170 < gc.GetPointerY(0))
            {
                Stage = 7;
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 330 && 30 < gc.GetPointerX(0) && gc.GetPointerY(0) < 320 && 20 < gc.GetPointerY(0) && PeopleED)
            {
                //一般人説明
                Stage = 8;
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 700 && 400 < gc.GetPointerX(0) && gc.GetPointerY(0) < 320 && 20 < gc.GetPointerY(0) && NeetED)
            {
                //ニート説明
                Stage = 9;
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 330 && 30 < gc.GetPointerX(0) && gc.GetPointerY(0) < 720 && 420 < gc.GetPointerY(0) && DoctorED)
            {
                //研究者説明
                Stage = 10;
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 700 && 400 < gc.GetPointerX(0) && gc.GetPointerY(0) < 720 && 420 < gc.GetPointerY(0) && MachoED)
            {
                //マッチョ説明
                Stage = 11;
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 330 && 30 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1120 && 820 < gc.GetPointerY(0) && HitmanED)
            {
                //ヒットマン説明
                Stage = 12;
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 700 && 400 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1120 && 820 < gc.GetPointerY(0) && YankeeED)
            {
                //遊び人説明
                Stage = 13;
            }
            
        }
        else if (Stage == 7)
        {
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 330 && 30 < gc.GetPointerX(0) && gc.GetPointerY(0) < 320 && 20 < gc.GetPointerY(0) && MadamED)
            {
                //マダム説明
                Stage = 14;
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 700 && 400 < gc.GetPointerX(0) && gc.GetPointerY(0) < 320 && 20 < gc.GetPointerY(0) && YushaED)
            {
                //勇者説明
                Stage = 15;
            }
            else if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 330 && 30 < gc.GetPointerX(0) && gc.GetPointerY(0) < 720 && 420 < gc.GetPointerY(0) && GirlED)
            {
                //美少女説明
                Stage = 16;
            }

            BackTitle();        
        }
        else if (Stage == 8)
        {
            //一般人説明
            BackTitle();
        }
        else if (Stage == 9)
        {
            //ニート説明
            BackTitle();
        }
        else if (Stage == 10)
        {
            //研究者説明
            BackTitle();
        }
        else if (Stage == 11)
        {
            //マッチョ説明
            BackTitle();
        }
        else if (Stage == 12)
        {
            //ヒットマン説明
            BackTitle();
        }
        else if (Stage == 13)
        {
            //遊び人説明
            BackTitle();
        }
        else if (Stage == 14)
        {
            //マダム説明
            BackTitle2();
        }
        else if (Stage == 15)
        {
            //勇者説明
            BackTitle2();
        }
        else if (Stage == 16)
        {
            //美少女説明
            BackTitle2();
        }
        else if (Stage == 17)
        {
            //操作説明
            if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 130 && 30 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1270 && 1170 < gc.GetPointerY(0))
            {
                ManStop = false;
                Man_x = 0;
                Stage = 0;
            }
        }
    }

    /// <summary>
    /// 描画の処理
    /// </summary>
    public override void DrawGame()
    {
        // 画面を白で塗りつぶす
        gc.ClearScreen();
        if (Stage == 0)
        {
            //スタート画面
            gc.DrawImage(GcImage.Title, 0, 100);
            gc.DrawImage(GcImage.Game, 170, 300);
            gc.SetFontSize(70);
            gc.DrawString("始める", 280, 700);
            gc.DrawString("操作説明", 240, 850);           
            gc.DrawString("図鑑", 300, 1000);

            if (Complete)
            {
                gc.DrawImage(GcImage.Trophydot, 200, 1000);
            }

            //アニメーション描画
            if (Man1)
            {
                gc.DrawImage(GcImage.Man1, Man_x, 1100);
            }
            else if (Man2)
            {
                gc.DrawImage(GcImage.Man2, Man_x, 1100);
            }
            else if (Man3)
            {
                gc.DrawImage(GcImage.Man3, Man_x, 1100);
            }
        }
        else if (Stage == 1)
        {
            //説明画面
            gc.SetFontSize(60);
            gc.DrawString("ある日突然棒人間が", 100, 400);
            gc.DrawString("現れた！", 250, 500);
            gc.DrawString("3日間で立派な", 150, 700);
            gc.DrawString("棒人間に育てよう！", 100, 800);
        }
        else if (Stage == 2)
        {
            //１日目
            //棒人間
            if (NormalMan)
            {
                gc.DrawImage(GcImage.Man, 50, 250);
            }
            else if (NaderuMan)
            {
                gc.DrawImage(GcImage.Naderu1, 50, 250);
            }
            else if (NaderuMan2)
            {
                gc.DrawImage(GcImage.Naderu2, 50, 250);
            }
            else if (TrainingMan)
            {
                gc.DrawImage(GcImage.Training1, 50, 250);
            }
            else if (TrainingMan2)
            {
                gc.DrawImage(GcImage.Training2, 50, 250);
            }
            else if (StudyMan)
            {
                gc.DrawImage(GcImage.Studys1, 50, 250);
            }
            else if (StudyMan2)
            {
                gc.DrawImage(GcImage.Studys2, 50, 250);
            }
            else if (TeaMan)
            {
                gc.DrawImage(GcImage.Tea1, 50, 250);
            }
            else if (TeaMan2)
            {
                gc.DrawImage(GcImage.Tea2, 50, 250);
            }
            else if (PlayMan)
            {
                gc.DrawImage(GcImage.Play1, 50, 250);
            }
            else if (PlayMan2)
            {
                gc.DrawImage(GcImage.Play2, 50, 250);
            }
            else if (SlapMan)
            {
                gc.DrawImage(GcImage.Manslap, 50, 250);
            }


            gc.SetFontSize(65);
            gc.DrawString(Day+"日目", 60, 130);
            gc.SetFontSize(35);
            gc.DrawString("あと", 340, 30);

            //行動回数
            if (MoveCount == 0)
            {
                gc.DrawImage(GcImage.Numberbox, 270, 50);
            }
            else if (MoveCount == 1)
            {
                gc.DrawImage(GcImage.Numberbox2, 270, 50);
            }
            else if (MoveCount == 2)
            {
                gc.DrawImage(GcImage.Numberbox1, 270, 50);
            }
            else if (MoveCount == 3)
            {
                gc.DrawImage(GcImage.Numberbox0, 270, 50);
            }

            //好感度
            if (LoveCount == 0)
            {
                gc.DrawImage(GcImage.Love0, 480, 50);
            }
            else if (LoveCount == 1)
            {
                gc.DrawImage(GcImage.Love1, 480, 50);
            }
            else if (LoveCount == 2)
            {
                gc.DrawImage(GcImage.Love2, 480, 50);
            }
            else if (LoveCount == 3)
            {
                gc.DrawImage(GcImage.Love3, 480, 50);
            }

            if (MoveCount < 3)
            {

                //コマンドボタン
                gc.SetFontSize(35);
                gc.DrawString("なでる", 50, 1100);
                gc.DrawImage(GcImage.Naderuicon, 40, 1150);
                gc.DrawString("筋トレ", 220, 1100);
                gc.DrawImage(GcImage.Trainingicon, 210, 1150);
                gc.DrawString("勉強", 410, 1100);
                gc.DrawImage(GcImage.Studyicon, 400, 1150);
                gc.DrawString("ビンタ", 560, 1100);
                gc.DrawImage(GcImage.Slapicon2, 560, 1150);

                if (LoveFull)
                {
                    gc.DrawString("お茶", 600, 300);
                    gc.DrawImage(GcImage.Teaicon, 590, 350);
                    gc.DrawString("遊ぶ", 600, 500);
                    gc.DrawImage(GcImage.Playicon, 590, 550);
                }
            }
            else
            {
                gc.SetFontSize(60);
                gc.DrawString("寝る", 300, 1100);
            }


        }
        else if (Stage == 3)
        {
            //間
            gc.SetFontSize(80);
            gc.DrawString("次の日…", 200, 500);
            gc.DrawString("TAP", 280, 800);
        }
        else if (Stage == 4)
        {
            //間
            gc.SetFontSize(60);
            gc.DrawString("棒人間の姿が", 170, 400);
            gc.DrawString("変わっている…！", 150, 550);
            gc.SetFontSize(80);
            gc.DrawString("TAP", 280, 800);
        }
        else if (Stage == 5)
        {
            gc.SetFontSize(50);
            gc.DrawImage(GcImage.Kira3, 0, 0);           
            gc.DrawString("なんと棒人間は", 200, 1020);               
            gc.SetFontSize(50);
            gc.DrawString("となった！", 250, 1200);
            gc.SetFontSize(60);
            gc.DrawString("RETRY", 500, 50);          

            if (Yusha)
            {
                if (YushaCount == 1)
                {
                    gc.DrawImage(GcImage.New, 60, 170);
                }
                YushaED = true;
                gc.SetFontSize(70);
                gc.DrawString("勇者", 300, 1100);
                gc.DrawImage(GcImage.Yusha, 50, 110);
            }
            else if (Neet)
            {
                if (NeetCount == 1)
                {
                    gc.DrawImage(GcImage.New, 60, 170);
                }
                NeetED = true;
                gc.SetFontSize(70);
                gc.DrawString("ニート", 275, 1100);
                gc.DrawImage(GcImage.Neet, 50, 80);
            }
            else if (Yankee)
            {
                if (YankeeCount == 1)
                {
                    gc.DrawImage(GcImage.New, 60, 170);
                }
                YankeeED = true;
                gc.SetFontSize(70);
                gc.DrawString("遊び人", 270, 1100);
                gc.DrawImage(GcImage.Yankee, 50, 80);
            }
            else if (Hitman)
            {
                if (HitmanCount == 1)
                {
                    gc.DrawImage(GcImage.New, 60, 170);
                }
                HitmanED = true;
                gc.SetFontSize(70);
                gc.DrawString("ヒットマン", 200, 1100);
                gc.DrawImage(GcImage.Hitman, 50, 80);
            }
            else if (Madam)
            {
                if (MadamCount == 1)
                {
                    gc.DrawImage(GcImage.New, 60, 170);
                }
                MadamED = true;
                gc.SetFontSize(70);
                gc.DrawString("マダム", 270, 1100);
                gc.DrawImage(GcImage.Madam, 50, 80);
            }
            else if (Doctor)
            {
                if (DoctorCount == 1)
                {
                    gc.DrawImage(GcImage.New, 60, 170);
                }
                DoctorED = true;
                gc.SetFontSize(70);
                gc.DrawString("研究者", 270, 1100);
                gc.DrawImage(GcImage.Doctor, 50, 80);
            }
            else if (Macho)
            {
                gc.DrawImage(GcImage.Macho2, 50, 80);
                if (MachoCount == 1)
                {
                    gc.DrawImage(GcImage.New, 60, 130);
                }
                MachoED = true;
                gc.SetFontSize(70);
                gc.DrawString("マッチョ", 230, 1100);              
            }
            else if (People)
            {
                if (PeopleCount == 1)
                {
                    gc.DrawImage(GcImage.New, 60, 170);
                }
                PeopleED = true;
                gc.SetFontSize(70);
                gc.DrawString("一般人", 270, 1100);
                gc.DrawImage(GcImage.People, 50, 80);
            }
            else if (Girl)
            {
                if (GirlCount == 1)
                {
                    gc.DrawImage(GcImage.New, 20, 330);
                }
                GirlED = true;
                gc.SetFontSize(70);
                gc.DrawString("美少女", 270, 1100);
                gc.DrawImage(GcImage.Bishojo2, 50, 80);
            }

        }
        else if (Stage == 6)
        {
            //ギャラリー

            //一般人
            if (PeopleED)
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Boxpeople, 30, 20);
                gc.DrawString("一般人", 110, 330);
            }
            else
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Mysterybox, 30, 20);
                gc.DrawString("一般人", 110, 330);
            }

            //ニート
            if (NeetED)
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Boxneet, 400, 20);
                gc.DrawString("ニート", 480, 330);
            }
            else
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Mysterybox, 400, 20);
                gc.DrawString("ニート", 480, 330);
            }

            //研究者
            if (DoctorED)
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Boxdoctor, 30, 420);
                gc.DrawString("研究者", 110, 730);
            }
            else
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Mysterybox, 30, 420);
                gc.DrawString("研究者", 110, 730);
            }

            //マッチョ
            if (MachoED)
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Boxmacho, 400, 420);
                gc.DrawString("マッチョ", 460, 730);
            }
            else
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Mysterybox, 400, 420);
                gc.DrawString("マッチョ", 460, 730);
            }

            //ヒットマン
            if (HitmanED)
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Boxhitman, 30, 820);
                gc.DrawString("ヒットマン", 70, 1130);
            }
            else
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Mysterybox, 30, 820);
                gc.DrawString("ヒットマン", 70, 1130);
            }

            //遊び人
            if (YankeeED)
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Boxyankee, 400, 820);
                gc.DrawString("遊び人", 480, 1130);
            }
            else
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Mysterybox, 400, 820);
                gc.DrawString("遊び人", 480, 1130);
            }

            gc.DrawImage(GcImage.Back, 30, 1175);
            gc.DrawImage(GcImage.NextR, 600, 1175);
        }
        else if (Stage == 7)
        {
            //ギャラリー2

            //マダム
            if (MadamED)
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Boxmadam, 30, 20);
                gc.DrawString("マダム", 110, 330);
            }
            else
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Mysterybox, 30, 20);
                gc.DrawString("マダム", 110, 330);
            }

            //勇者
            if (YushaED)
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Boxyusha, 400, 20);
                gc.DrawString("勇者", 500, 330);
            }
            else
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Mysterybox, 400, 20);
                gc.DrawString("勇者", 500, 330);
            }

            //美少女
            if (GirlED)
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Boxbishojo, 30, 420);
                gc.DrawString("美少女", 110, 730);
            }
            else
            {
                gc.SetFontSize(45);
                gc.DrawImage(GcImage.Mysterybox, 30, 420);
                gc.DrawString("？？？", 110, 730);
            }

            //トロフィー
            if (Complete)
            {
                gc.DrawImage(GcImage.Trophy, 280, 850);
                gc.SetFontSize(65);
                gc.DrawString("COMPLETE!", 230, 1080);
            }


            gc.DrawImage(GcImage.NextL, 30, 1175);
        }
        else if (Stage == 8)
        {
            //一般人説明
            gc.DrawImage(GcImage.People, 50, 150);
            gc.SetFontSize(75);
            gc.DrawString("<一般人>", 220, 60);

            gc.SetFontSize(50);
            gc.DrawString("ただの一般人。", 200, 970);
            gc.DrawString("特に特徴はない。", 180, 1070);

            gc.DrawImage(GcImage.Back, 30, 1175);
        }
        else if (Stage == 9)
        {
            //ニート説明
            gc.DrawImage(GcImage.Neet, 50, 50);
            gc.SetFontSize(75);
            gc.DrawString("<ニート>", 220, 60);

            gc.SetFontSize(50);
            gc.DrawString("働かないという", 170, 870);
            gc.DrawString("固い意志を持っている。", 100, 970);
            gc.DrawString("働かない以外に特徴はない。", 50, 1070);

            gc.DrawImage(GcImage.Back, 30, 1175);
        }
        else if (Stage == 10)
        {
            //研究者説明
            gc.DrawImage(GcImage.Doctor, 50, 150);
            gc.SetFontSize(75);
            gc.DrawString("<研究者>", 220, 60);

            gc.SetFontSize(50);
            gc.DrawString("何かの研究をしている。", 100, 970);
            gc.DrawString("何の研究かは分からない。", 80, 1070);

            gc.DrawImage(GcImage.Back, 30, 1175);
        }
        else if (Stage == 11)
        {
            //マッチョ説明
            gc.DrawImage(GcImage.Macho2, 50, 150);
            gc.SetFontSize(75);
            gc.DrawString("<マッチョ>", 200, 60);

            gc.SetFontSize(50);
            gc.DrawString("毎日筋トレは欠かさない。", 80, 970);
            gc.DrawString("もはや棒ではなくなった。", 80, 1070);

            gc.DrawImage(GcImage.Back, 30, 1175);
        }
        else if (Stage == 12)
        {
            //ヒットマン説明
            gc.DrawImage(GcImage.Hitman, 50, 150);
            gc.SetFontSize(75);
            gc.DrawString("<ヒットマン>", 150, 60);

            gc.SetFontSize(50);
            gc.DrawString("歴戦のヒットマン。", 140, 970);
            gc.DrawString("くぐった修羅場は数知れず。", 60, 1070);

            gc.DrawImage(GcImage.Back, 30, 1175);
        }
        else if (Stage == 13)
        {
            //遊び人説明
            gc.DrawImage(GcImage.Yankee, 50, 150);
            gc.SetFontSize(75);
            gc.DrawString("<遊び人>", 220, 60);

            gc.SetFontSize(50);
            gc.DrawString("とにかくチャラい。", 140, 970);
            gc.DrawString("知らない遊びはないらしい。", 50, 1070);

            gc.DrawImage(GcImage.Back, 30, 1175);
        }
        else if (Stage == 14)
        {
            //マダム説明
            gc.DrawImage(GcImage.Madam, 50, 180);
            gc.SetFontSize(75);
            gc.DrawString("<マダム>", 220, 60);

            gc.SetFontSize(50);
            gc.DrawString("高貴なマダム。", 200, 970);
            gc.DrawString("美に関してかなり敏感。", 100, 1070);

            gc.DrawImage(GcImage.Back, 30, 1175);
        }
        else if (Stage == 15)
        {
            //勇者説明
            gc.DrawImage(GcImage.Yusha, 50, 150);
            gc.SetFontSize(75);
            gc.DrawString("<勇者>", 240, 60);

            gc.SetFontSize(50);
            gc.DrawString("自称伝説の勇者。", 180, 970);
            gc.DrawString("本物かどうかは誰も知らない。", 30, 1070);

            gc.DrawImage(GcImage.Back, 30, 1175);
        }
        else if (Stage == 16)
        {
            //美少女説明
            gc.DrawImage(GcImage.Bishojo2, 50, 150);
            gc.SetFontSize(75);
            gc.DrawString("<美少女>", 220, 60);

            gc.SetFontSize(50);
            gc.DrawString("絶世の美少女。", 220, 970);
            gc.DrawString("その存在は謎に包まれている。", 30, 1070);

            gc.DrawImage(GcImage.Back, 30, 1175);
        }
        else if (Stage == 17)
        {
            //操作説明
            gc.SetFontSize(75);
            gc.DrawString("<操作説明>", 180, 30);

            gc.DrawImage(GcImage.Love0, 250, 100);
            gc.SetFontSize(50);
            gc.DrawString("・好感度", 50, 180);
            gc.SetFontSize(40);
            gc.DrawString("特定の行動の順番で上下します。", 50, 280);
         
            gc.SetFontSize(50);
            gc.DrawString("・行動回数", 50, 430);
            gc.SetFontSize(40);
            gc.DrawString("1日で行動できる回数は3回まで。", 50, 530);
            gc.DrawString("(3日間で育成先が決まります)", 50,580);

            gc.DrawImage(GcImage.Naderuicon, 450, 700);
            gc.DrawImage(GcImage.Trainingicon, 550, 700);
            gc.SetFontSize(50);
            gc.DrawString("・コマンド(行動)", 50, 730);
            gc.SetFontSize(40);
            gc.DrawString("タップで選択します。", 50, 830);
            gc.DrawString("この行動と好感度によって、", 50, 890);
            gc.DrawString("棒人間の成長先が変わります。", 50, 950);

            gc.SetFontSize(45);
            gc.DrawString("図鑑のコンプリートを目指そう！", 30, 1050);

            gc.DrawImage(GcImage.Back, 30, 1175);

        }
    }
    

    public void NormalManDraw()
    {
        SlapMan = false;
        NaderuMan = false;
        NaderuMan2 = false;
        TrainingMan2 = false;
        TrainingMan = false;
        StudyMan2 = false;
        StudyMan = false;
        TeaMan2 = false;
        TeaMan = false;
        PlayMan2 = false;
        PlayMan = false;
        NormalMan = true;
        MoveOK = true;
        MoveCount++;
    }

    //なでるアニメ
    public void NaderuManDraw()
    {
        AnimationCount++;
        NaderuMan2 = true;
        NaderuMan = false;
        Invoke(nameof(NaderuManDraw2), 0.2f);
    }

    public void NaderuManDraw2()
    {
        if (AnimationCount < 3)
        {
            NaderuMan2 = false;
            NaderuMan = true;
            Invoke(nameof(NaderuManDraw), 0.2f);
        }
        else
        {
            NormalManDraw();
            AnimationCount = 0;
        }
        
    }

    //筋トレアニメ
    public void TrainingManDraw()
    {
        AnimationCount++;
        TrainingMan2 = true;
        TrainingMan = false;
        Invoke(nameof(TrainingManDraw2), 0.3f);
    }

    public void TrainingManDraw2()
    {
        if (AnimationCount < 3)
        {
            TrainingMan2 = false;
            TrainingMan = true;
            Invoke(nameof(TrainingManDraw), 0.3f);
        }
        else
        {
            NormalManDraw();
            AnimationCount = 0;
        }

    }

    //勉強アニメ
    public void StudyManDraw()
    {
        AnimationCount++;
        StudyMan2 = false;
        StudyMan = true;
        Invoke(nameof(StudyManDraw2), 0.3f);
    }

    public void StudyManDraw2()
    {
        if (AnimationCount < 3)
        {
            StudyMan2 = true;
            StudyMan = false;
            Invoke(nameof(StudyManDraw), 0.3f);
        }
        else
        {
            NormalManDraw();
            AnimationCount = 0;
        }

    }


    //お茶アニメ
    public void TeaManDraw()
    {
        AnimationCount++;
        TeaMan2 = false;
        TeaMan = true;
        Invoke(nameof(TeaManDraw2), 0.8f);
    }

    public void TeaManDraw2()
    {
        if (AnimationCount < 2)
        {
            TeaMan2 = true;
            TeaMan = false;
            Invoke(nameof(TeaManDraw), 0.6f);
        }
        else
        {
            NormalManDraw();
            AnimationCount = 0;
        }

    }

    //遊ぶアニメ
    public void PlayManDraw()
    {
        AnimationCount++;
        PlayMan2 = false;
        PlayMan = true;
        Invoke(nameof(PlayManDraw2), 0.2f);
    }

    public void PlayManDraw2()
    {
        if (AnimationCount < 3)
        {
            PlayMan2 = true;
            PlayMan = false;
            Invoke(nameof(PlayManDraw), 0.2f);
        }
        else
        {
            NormalManDraw();
            AnimationCount = 0;
        }

    }

    //タイトルアニメ
    public void ManDraw()
    {
        if (!ManStop)
        {
            Man_x += 5;
            Man1 = true;
            Man2 = false;
            Man3 = false;
            Invoke(nameof(ManDraw2), 0.2f);
        }
    }

    public void ManDraw2()
    {
        if (!ManStop)
        {
            Man_x += 5;
            Man1 = false;
            Man2 = true;
            Man3 = false;
            Invoke(nameof(ManDraw3), 0.2f);
        }   
    }

    public void ManDraw3()
    {
        if (!ManStop)
        {
            Man_x += 5;
            Man1 = false;
            Man2 = false;
            Man3 = true;
            Invoke(nameof(ManDraw), 0.2f);
        }
    }

    //初期化処理
    public void Reset()
    {
        Day = 1;
        MovePast = 0;
        LoveCount = 0;
        MoveCount = 0;
        MoveDV = false;

        MoveNaderu = 0;
        MoveStudy = 0;
        MoveSlap = 0;
        MovePlay = 0;
        MoveTea = 0;
        MoveTraining = 0;

        Yankee = false;
        Yusha = false;
        Macho = false;
        Madam = false;
        Doctor = false;
        People = false;
        Hitman = false;
        Neet = false;
    }

    public void BackTitle()
    {
        if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 130 && 30 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1270 && 1170 < gc.GetPointerY(0))
        {
            Stage = 6;
        }
    }

    public void BackTitle2()
    {
        if (gc.GetPointerFrameCount(0) == 1 && gc.GetPointerX(0) < 130 && 30 < gc.GetPointerX(0) && gc.GetPointerY(0) < 1270 && 1170 < gc.GetPointerY(0))
        {
            Stage = 7;
        }
    }
}

