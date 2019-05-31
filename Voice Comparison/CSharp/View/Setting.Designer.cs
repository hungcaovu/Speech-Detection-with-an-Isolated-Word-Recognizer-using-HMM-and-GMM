namespace Voice_Comparasion
{
    partial class Setting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Mfcc = new System.Windows.Forms.TabControl();
            this.tabMfccSetting = new System.Windows.Forms.TabPage();
            this.highfreq_hz_lb = new System.Windows.Forms.Label();
            this.lowfreq_hz_lb = new System.Windows.Forms.Label();
            this.timeshift_ms_lb = new System.Windows.Forms.Label();
            this.timeframems_lb = new System.Windows.Forms.Label();
            this.cepfilter_tb = new System.Windows.Forms.TextBox();
            this.highfreq_tb = new System.Windows.Forms.TextBox();
            this.numceps_tb = new System.Windows.Forms.TextBox();
            this.lowfreq_tb = new System.Windows.Forms.TextBox();
            this.cepfilter_lb = new System.Windows.Forms.Label();
            this.timeshift_tb = new System.Windows.Forms.TextBox();
            this.hightfreq_lb = new System.Windows.Forms.Label();
            this.numceps_lb = new System.Windows.Forms.Label();
            this.timeframe_tb = new System.Windows.Forms.TextBox();
            this.lowfreq_lb = new System.Windows.Forms.Label();
            this.timeshift_lb = new System.Windows.Forms.Label();
            this.timeframe_lb = new System.Windows.Forms.Label();
            this.tabPitch = new System.Windows.Forms.TabPage();
            this.removeUnpitch_cb = new System.Windows.Forms.CheckBox();
            this.median_cb = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pitchtype_cbx = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.yinThreshold_tbx = new System.Windows.Forms.TextBox();
            this.median_tbl = new System.Windows.Forms.TextBox();
            this.hightFreq_tbx = new System.Windows.Forms.TextBox();
            this.lowFreq_tbx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timeshift_tbl = new System.Windows.Forms.TextBox();
            this.window_lb = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timeframe_tbl = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.remove_noise_cbx = new System.Windows.Forms.CheckBox();
            this.shiftToZero_cbx = new System.Windows.Forms.CheckBox();
            this.normal_audio_cbx = new System.Windows.Forms.CheckBox();
            this.tabLogSetting = new System.Windows.Forms.TabPage();
            this.logLevel_cbx = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.enanblelog_cbx = new System.Windows.Forms.CheckBox();
            this.separate_log_cbx = new System.Windows.Forms.CheckBox();
            this.tabVad = new System.Windows.Forms.TabPage();
            this.pitch_tbx = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.energy_txb = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabTraining = new System.Windows.Forms.TabPage();
            this.gmmCoVarType_cbx = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.gmmCompNum_tbx = new System.Windows.Forms.TextBox();
            this.hmmStateNum_tbx = new System.Windows.Forms.TextBox();
            this.dataType_cbx = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.typeCep_lb = new System.Windows.Forms.Label();
            this.apply_btn = new System.Windows.Forms.Button();
            this.ok_btn = new System.Windows.Forms.Button();
            this.useStandardization_cbx = new System.Windows.Forms.CheckBox();
            this.Mfcc.SuspendLayout();
            this.tabMfccSetting.SuspendLayout();
            this.tabPitch.SuspendLayout();
            this.tabSetting.SuspendLayout();
            this.tabLogSetting.SuspendLayout();
            this.tabVad.SuspendLayout();
            this.tabTraining.SuspendLayout();
            this.SuspendLayout();
            // 
            // Mfcc
            // 
            this.Mfcc.Controls.Add(this.tabMfccSetting);
            this.Mfcc.Controls.Add(this.tabPitch);
            this.Mfcc.Controls.Add(this.tabSetting);
            this.Mfcc.Controls.Add(this.tabLogSetting);
            this.Mfcc.Controls.Add(this.tabVad);
            this.Mfcc.Controls.Add(this.tabTraining);
            this.Mfcc.Dock = System.Windows.Forms.DockStyle.Top;
            this.Mfcc.Location = new System.Drawing.Point(0, 0);
            this.Mfcc.Name = "Mfcc";
            this.Mfcc.SelectedIndex = 0;
            this.Mfcc.Size = new System.Drawing.Size(423, 169);
            this.Mfcc.TabIndex = 0;
            // 
            // tabMfccSetting
            // 
            this.tabMfccSetting.Controls.Add(this.useStandardization_cbx);
            this.tabMfccSetting.Controls.Add(this.highfreq_hz_lb);
            this.tabMfccSetting.Controls.Add(this.lowfreq_hz_lb);
            this.tabMfccSetting.Controls.Add(this.timeshift_ms_lb);
            this.tabMfccSetting.Controls.Add(this.timeframems_lb);
            this.tabMfccSetting.Controls.Add(this.cepfilter_tb);
            this.tabMfccSetting.Controls.Add(this.highfreq_tb);
            this.tabMfccSetting.Controls.Add(this.numceps_tb);
            this.tabMfccSetting.Controls.Add(this.lowfreq_tb);
            this.tabMfccSetting.Controls.Add(this.cepfilter_lb);
            this.tabMfccSetting.Controls.Add(this.timeshift_tb);
            this.tabMfccSetting.Controls.Add(this.hightfreq_lb);
            this.tabMfccSetting.Controls.Add(this.numceps_lb);
            this.tabMfccSetting.Controls.Add(this.timeframe_tb);
            this.tabMfccSetting.Controls.Add(this.lowfreq_lb);
            this.tabMfccSetting.Controls.Add(this.timeshift_lb);
            this.tabMfccSetting.Controls.Add(this.timeframe_lb);
            this.tabMfccSetting.Location = new System.Drawing.Point(4, 22);
            this.tabMfccSetting.Name = "tabMfccSetting";
            this.tabMfccSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabMfccSetting.Size = new System.Drawing.Size(415, 143);
            this.tabMfccSetting.TabIndex = 0;
            this.tabMfccSetting.Text = "Mfcc Setting";
            this.tabMfccSetting.UseVisualStyleBackColor = true;
            // 
            // highfreq_hz_lb
            // 
            this.highfreq_hz_lb.AutoSize = true;
            this.highfreq_hz_lb.Location = new System.Drawing.Point(387, 53);
            this.highfreq_hz_lb.Name = "highfreq_hz_lb";
            this.highfreq_hz_lb.Size = new System.Drawing.Size(18, 13);
            this.highfreq_hz_lb.TabIndex = 20;
            this.highfreq_hz_lb.Text = "hz";
            // 
            // lowfreq_hz_lb
            // 
            this.lowfreq_hz_lb.AutoSize = true;
            this.lowfreq_hz_lb.Location = new System.Drawing.Point(385, 16);
            this.lowfreq_hz_lb.Name = "lowfreq_hz_lb";
            this.lowfreq_hz_lb.Size = new System.Drawing.Size(18, 13);
            this.lowfreq_hz_lb.TabIndex = 19;
            this.lowfreq_hz_lb.Text = "hz";
            // 
            // timeshift_ms_lb
            // 
            this.timeshift_ms_lb.AutoSize = true;
            this.timeshift_ms_lb.Location = new System.Drawing.Point(160, 53);
            this.timeshift_ms_lb.Name = "timeshift_ms_lb";
            this.timeshift_ms_lb.Size = new System.Drawing.Size(20, 13);
            this.timeshift_ms_lb.TabIndex = 18;
            this.timeshift_ms_lb.Text = "ms";
            // 
            // timeframems_lb
            // 
            this.timeframems_lb.AutoSize = true;
            this.timeframems_lb.Location = new System.Drawing.Point(160, 16);
            this.timeframems_lb.Name = "timeframems_lb";
            this.timeframems_lb.Size = new System.Drawing.Size(20, 13);
            this.timeframems_lb.TabIndex = 17;
            this.timeframems_lb.Text = "ms";
            // 
            // cepfilter_tb
            // 
            this.cepfilter_tb.Location = new System.Drawing.Point(309, 86);
            this.cepfilter_tb.Name = "cepfilter_tb";
            this.cepfilter_tb.Size = new System.Drawing.Size(69, 20);
            this.cepfilter_tb.TabIndex = 15;
            // 
            // highfreq_tb
            // 
            this.highfreq_tb.Location = new System.Drawing.Point(309, 49);
            this.highfreq_tb.Name = "highfreq_tb";
            this.highfreq_tb.Size = new System.Drawing.Size(69, 20);
            this.highfreq_tb.TabIndex = 16;
            // 
            // numceps_tb
            // 
            this.numceps_tb.Location = new System.Drawing.Point(84, 86);
            this.numceps_tb.Name = "numceps_tb";
            this.numceps_tb.Size = new System.Drawing.Size(69, 20);
            this.numceps_tb.TabIndex = 14;
            // 
            // lowfreq_tb
            // 
            this.lowfreq_tb.Location = new System.Drawing.Point(309, 12);
            this.lowfreq_tb.Name = "lowfreq_tb";
            this.lowfreq_tb.Size = new System.Drawing.Size(69, 20);
            this.lowfreq_tb.TabIndex = 13;
            // 
            // cepfilter_lb
            // 
            this.cepfilter_lb.AutoSize = true;
            this.cepfilter_lb.Location = new System.Drawing.Point(235, 90);
            this.cepfilter_lb.Name = "cepfilter_lb";
            this.cepfilter_lb.Size = new System.Drawing.Size(62, 13);
            this.cepfilter_lb.TabIndex = 9;
            this.cepfilter_lb.Text = "Ceps Filter :";
            // 
            // timeshift_tb
            // 
            this.timeshift_tb.Location = new System.Drawing.Point(84, 49);
            this.timeshift_tb.Name = "timeshift_tb";
            this.timeshift_tb.Size = new System.Drawing.Size(69, 20);
            this.timeshift_tb.TabIndex = 12;
            // 
            // hightfreq_lb
            // 
            this.hightfreq_lb.AutoSize = true;
            this.hightfreq_lb.Location = new System.Drawing.Point(235, 53);
            this.hightfreq_lb.Name = "hightfreq_lb";
            this.hightfreq_lb.Size = new System.Drawing.Size(59, 13);
            this.hightfreq_lb.TabIndex = 8;
            this.hightfreq_lb.Text = "High Freq :";
            // 
            // numceps_lb
            // 
            this.numceps_lb.AutoSize = true;
            this.numceps_lb.Location = new System.Drawing.Point(10, 90);
            this.numceps_lb.Name = "numceps_lb";
            this.numceps_lb.Size = new System.Drawing.Size(62, 13);
            this.numceps_lb.TabIndex = 7;
            this.numceps_lb.Text = "Num Ceps :";
            // 
            // timeframe_tb
            // 
            this.timeframe_tb.Location = new System.Drawing.Point(84, 12);
            this.timeframe_tb.Name = "timeframe_tb";
            this.timeframe_tb.Size = new System.Drawing.Size(69, 20);
            this.timeframe_tb.TabIndex = 11;
            // 
            // lowfreq_lb
            // 
            this.lowfreq_lb.AutoSize = true;
            this.lowfreq_lb.Location = new System.Drawing.Point(235, 16);
            this.lowfreq_lb.Name = "lowfreq_lb";
            this.lowfreq_lb.Size = new System.Drawing.Size(57, 13);
            this.lowfreq_lb.TabIndex = 6;
            this.lowfreq_lb.Text = "Low Freq :";
            // 
            // timeshift_lb
            // 
            this.timeshift_lb.AutoSize = true;
            this.timeshift_lb.Location = new System.Drawing.Point(10, 53);
            this.timeshift_lb.Name = "timeshift_lb";
            this.timeshift_lb.Size = new System.Drawing.Size(60, 13);
            this.timeshift_lb.TabIndex = 10;
            this.timeshift_lb.Text = "Time Shift :";
            // 
            // timeframe_lb
            // 
            this.timeframe_lb.AutoSize = true;
            this.timeframe_lb.Location = new System.Drawing.Point(10, 16);
            this.timeframe_lb.Name = "timeframe_lb";
            this.timeframe_lb.Size = new System.Drawing.Size(68, 13);
            this.timeframe_lb.TabIndex = 5;
            this.timeframe_lb.Text = "Time Frame :";
            // 
            // tabPitch
            // 
            this.tabPitch.Controls.Add(this.removeUnpitch_cb);
            this.tabPitch.Controls.Add(this.median_cb);
            this.tabPitch.Controls.Add(this.comboBox1);
            this.tabPitch.Controls.Add(this.pitchtype_cbx);
            this.tabPitch.Controls.Add(this.label1);
            this.tabPitch.Controls.Add(this.label2);
            this.tabPitch.Controls.Add(this.label3);
            this.tabPitch.Controls.Add(this.label4);
            this.tabPitch.Controls.Add(this.yinThreshold_tbx);
            this.tabPitch.Controls.Add(this.median_tbl);
            this.tabPitch.Controls.Add(this.hightFreq_tbx);
            this.tabPitch.Controls.Add(this.lowFreq_tbx);
            this.tabPitch.Controls.Add(this.label5);
            this.tabPitch.Controls.Add(this.timeshift_tbl);
            this.tabPitch.Controls.Add(this.window_lb);
            this.tabPitch.Controls.Add(this.label13);
            this.tabPitch.Controls.Add(this.label6);
            this.tabPitch.Controls.Add(this.label7);
            this.tabPitch.Controls.Add(this.timeframe_tbl);
            this.tabPitch.Controls.Add(this.label8);
            this.tabPitch.Controls.Add(this.label9);
            this.tabPitch.Controls.Add(this.label10);
            this.tabPitch.Location = new System.Drawing.Point(4, 22);
            this.tabPitch.Name = "tabPitch";
            this.tabPitch.Padding = new System.Windows.Forms.Padding(3);
            this.tabPitch.Size = new System.Drawing.Size(415, 143);
            this.tabPitch.TabIndex = 1;
            this.tabPitch.Text = "Pitch Setting";
            this.tabPitch.UseVisualStyleBackColor = true;
            // 
            // removeUnpitch_cb
            // 
            this.removeUnpitch_cb.AutoSize = true;
            this.removeUnpitch_cb.Location = new System.Drawing.Point(230, 120);
            this.removeUnpitch_cb.Name = "removeUnpitch_cb";
            this.removeUnpitch_cb.Size = new System.Drawing.Size(107, 17);
            this.removeUnpitch_cb.TabIndex = 42;
            this.removeUnpitch_cb.Text = "Remove UnPitch";
            this.removeUnpitch_cb.UseVisualStyleBackColor = true;
            // 
            // median_cb
            // 
            this.median_cb.AutoSize = true;
            this.median_cb.Location = new System.Drawing.Point(230, 94);
            this.median_cb.Name = "median_cb";
            this.median_cb.Size = new System.Drawing.Size(61, 17);
            this.median_cb.TabIndex = 42;
            this.median_cb.Text = "Median";
            this.median_cb.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "YIN",
            "AMDF"});
            this.comboBox1.Location = new System.Drawing.Point(84, 90);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(96, 21);
            this.comboBox1.TabIndex = 41;
            // 
            // pitchtype_cbx
            // 
            this.pitchtype_cbx.FormattingEnabled = true;
            this.pitchtype_cbx.Items.AddRange(new object[] {
            "YIN",
            "AMDF",
            "ZERO CROSSING"});
            this.pitchtype_cbx.Location = new System.Drawing.Point(84, 12);
            this.pitchtype_cbx.Name = "pitchtype_cbx";
            this.pitchtype_cbx.Size = new System.Drawing.Size(96, 21);
            this.pitchtype_cbx.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(381, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "hz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "hz";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "ms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "ms";
            // 
            // yinThreshold_tbx
            // 
            this.yinThreshold_tbx.Location = new System.Drawing.Point(304, 12);
            this.yinThreshold_tbx.Name = "yinThreshold_tbx";
            this.yinThreshold_tbx.Size = new System.Drawing.Size(69, 20);
            this.yinThreshold_tbx.TabIndex = 33;
            // 
            // median_tbl
            // 
            this.median_tbl.Location = new System.Drawing.Point(304, 90);
            this.median_tbl.Name = "median_tbl";
            this.median_tbl.Size = new System.Drawing.Size(36, 20);
            this.median_tbl.TabIndex = 34;
            // 
            // hightFreq_tbx
            // 
            this.hightFreq_tbx.Location = new System.Drawing.Point(303, 64);
            this.hightFreq_tbx.Name = "hightFreq_tbx";
            this.hightFreq_tbx.Size = new System.Drawing.Size(69, 20);
            this.hightFreq_tbx.TabIndex = 34;
            // 
            // lowFreq_tbx
            // 
            this.lowFreq_tbx.Location = new System.Drawing.Point(303, 39);
            this.lowFreq_tbx.Name = "lowFreq_tbx";
            this.lowFreq_tbx.Size = new System.Drawing.Size(69, 20);
            this.lowFreq_tbx.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Yin Threshold:";
            // 
            // timeshift_tbl
            // 
            this.timeshift_tbl.Location = new System.Drawing.Point(84, 64);
            this.timeshift_tbl.Name = "timeshift_tbl";
            this.timeshift_tbl.Size = new System.Drawing.Size(69, 20);
            this.timeshift_tbl.TabIndex = 31;
            // 
            // window_lb
            // 
            this.window_lb.AutoSize = true;
            this.window_lb.Location = new System.Drawing.Point(346, 95);
            this.window_lb.Name = "window_lb";
            this.window_lb.Size = new System.Drawing.Size(46, 13);
            this.window_lb.TabIndex = 26;
            this.window_lb.Text = "Window";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 94);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Smoothing";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(229, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "High Freq :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Pitch Type";
            // 
            // timeframe_tbl
            // 
            this.timeframe_tbl.Location = new System.Drawing.Point(84, 39);
            this.timeframe_tbl.Name = "timeframe_tbl";
            this.timeframe_tbl.Size = new System.Drawing.Size(69, 20);
            this.timeframe_tbl.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(229, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Low Freq :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Time Shift :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Time Frame :";
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.remove_noise_cbx);
            this.tabSetting.Controls.Add(this.shiftToZero_cbx);
            this.tabSetting.Controls.Add(this.normal_audio_cbx);
            this.tabSetting.Location = new System.Drawing.Point(4, 22);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetting.Size = new System.Drawing.Size(415, 143);
            this.tabSetting.TabIndex = 2;
            this.tabSetting.Text = "Other Setting";
            this.tabSetting.UseVisualStyleBackColor = true;
            // 
            // remove_noise_cbx
            // 
            this.remove_noise_cbx.AutoSize = true;
            this.remove_noise_cbx.Location = new System.Drawing.Point(8, 30);
            this.remove_noise_cbx.Name = "remove_noise_cbx";
            this.remove_noise_cbx.Size = new System.Drawing.Size(151, 17);
            this.remove_noise_cbx.TabIndex = 1;
            this.remove_noise_cbx.Text = "Remove Noise Your Voice";
            this.remove_noise_cbx.UseVisualStyleBackColor = true;
            // 
            // shiftToZero_cbx
            // 
            this.shiftToZero_cbx.AutoSize = true;
            this.shiftToZero_cbx.Location = new System.Drawing.Point(8, 53);
            this.shiftToZero_cbx.Name = "shiftToZero_cbx";
            this.shiftToZero_cbx.Size = new System.Drawing.Size(126, 17);
            this.shiftToZero_cbx.TabIndex = 0;
            this.shiftToZero_cbx.Text = "Shift Sample To Zero";
            this.shiftToZero_cbx.UseVisualStyleBackColor = true;
            // 
            // normal_audio_cbx
            // 
            this.normal_audio_cbx.AutoSize = true;
            this.normal_audio_cbx.Location = new System.Drawing.Point(8, 6);
            this.normal_audio_cbx.Name = "normal_audio_cbx";
            this.normal_audio_cbx.Size = new System.Drawing.Size(125, 17);
            this.normal_audio_cbx.TabIndex = 0;
            this.normal_audio_cbx.Text = "Nornalize Your Voice";
            this.normal_audio_cbx.UseVisualStyleBackColor = true;
            // 
            // tabLogSetting
            // 
            this.tabLogSetting.Controls.Add(this.logLevel_cbx);
            this.tabLogSetting.Controls.Add(this.label17);
            this.tabLogSetting.Controls.Add(this.enanblelog_cbx);
            this.tabLogSetting.Controls.Add(this.separate_log_cbx);
            this.tabLogSetting.Location = new System.Drawing.Point(4, 22);
            this.tabLogSetting.Name = "tabLogSetting";
            this.tabLogSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogSetting.Size = new System.Drawing.Size(415, 143);
            this.tabLogSetting.TabIndex = 3;
            this.tabLogSetting.Text = "Log Setting";
            this.tabLogSetting.UseVisualStyleBackColor = true;
            // 
            // logLevel_cbx
            // 
            this.logLevel_cbx.FormattingEnabled = true;
            this.logLevel_cbx.Items.AddRange(new object[] {
            "NONE",
            "STEP",
            "INFORMATION",
            "DETAIL",
            "DATA"});
            this.logLevel_cbx.Location = new System.Drawing.Point(289, 6);
            this.logLevel_cbx.Name = "logLevel_cbx";
            this.logLevel_cbx.Size = new System.Drawing.Size(103, 21);
            this.logLevel_cbx.TabIndex = 44;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(225, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(54, 13);
            this.label17.TabIndex = 43;
            this.label17.Text = "Log Level";
            // 
            // enanblelog_cbx
            // 
            this.enanblelog_cbx.AutoSize = true;
            this.enanblelog_cbx.Location = new System.Drawing.Point(8, 6);
            this.enanblelog_cbx.Name = "enanblelog_cbx";
            this.enanblelog_cbx.Size = new System.Drawing.Size(80, 17);
            this.enanblelog_cbx.TabIndex = 1;
            this.enanblelog_cbx.Text = "Enable Log";
            this.enanblelog_cbx.UseVisualStyleBackColor = true;
            // 
            // separate_log_cbx
            // 
            this.separate_log_cbx.AutoSize = true;
            this.separate_log_cbx.Location = new System.Drawing.Point(8, 28);
            this.separate_log_cbx.Name = "separate_log_cbx";
            this.separate_log_cbx.Size = new System.Drawing.Size(157, 17);
            this.separate_log_cbx.TabIndex = 1;
            this.separate_log_cbx.Text = "Separate log each compare";
            this.separate_log_cbx.UseVisualStyleBackColor = true;
            // 
            // tabVad
            // 
            this.tabVad.Controls.Add(this.pitch_tbx);
            this.tabVad.Controls.Add(this.label12);
            this.tabVad.Controls.Add(this.energy_txb);
            this.tabVad.Controls.Add(this.label11);
            this.tabVad.Location = new System.Drawing.Point(4, 22);
            this.tabVad.Name = "tabVad";
            this.tabVad.Padding = new System.Windows.Forms.Padding(3);
            this.tabVad.Size = new System.Drawing.Size(415, 143);
            this.tabVad.TabIndex = 4;
            this.tabVad.Text = "VAD";
            this.tabVad.UseVisualStyleBackColor = true;
            // 
            // pitch_tbx
            // 
            this.pitch_tbx.Location = new System.Drawing.Point(109, 38);
            this.pitch_tbx.Name = "pitch_tbx";
            this.pitch_tbx.Size = new System.Drawing.Size(69, 20);
            this.pitch_tbx.TabIndex = 37;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "Pitch Threshold:";
            // 
            // energy_txb
            // 
            this.energy_txb.Location = new System.Drawing.Point(109, 6);
            this.energy_txb.Name = "energy_txb";
            this.energy_txb.Size = new System.Drawing.Size(69, 20);
            this.energy_txb.TabIndex = 35;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "Energy Threshold:";
            // 
            // tabTraining
            // 
            this.tabTraining.Controls.Add(this.gmmCoVarType_cbx);
            this.tabTraining.Controls.Add(this.label16);
            this.tabTraining.Controls.Add(this.label15);
            this.tabTraining.Controls.Add(this.gmmCompNum_tbx);
            this.tabTraining.Controls.Add(this.hmmStateNum_tbx);
            this.tabTraining.Controls.Add(this.dataType_cbx);
            this.tabTraining.Controls.Add(this.label14);
            this.tabTraining.Controls.Add(this.typeCep_lb);
            this.tabTraining.Location = new System.Drawing.Point(4, 22);
            this.tabTraining.Name = "tabTraining";
            this.tabTraining.Padding = new System.Windows.Forms.Padding(3);
            this.tabTraining.Size = new System.Drawing.Size(415, 143);
            this.tabTraining.TabIndex = 5;
            this.tabTraining.Text = "Training";
            this.tabTraining.UseVisualStyleBackColor = true;
            // 
            // gmmCoVarType_cbx
            // 
            this.gmmCoVarType_cbx.FormattingEnabled = true;
            this.gmmCoVarType_cbx.Items.AddRange(new object[] {
            "Single Value",
            "Diagonal Matrix",
            "Full Matrix"});
            this.gmmCoVarType_cbx.Location = new System.Drawing.Point(309, 12);
            this.gmmCoVarType_cbx.Name = "gmmCoVarType_cbx";
            this.gmmCoVarType_cbx.Size = new System.Drawing.Size(98, 21);
            this.gmmCoVarType_cbx.TabIndex = 46;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(208, 15);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(95, 13);
            this.label16.TabIndex = 45;
            this.label16.Text = "GMM CoVar Type:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 85);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(91, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "GMM Comp Num:";
            // 
            // gmmCompNum_tbx
            // 
            this.gmmCompNum_tbx.Location = new System.Drawing.Point(105, 82);
            this.gmmCompNum_tbx.Name = "gmmCompNum_tbx";
            this.gmmCompNum_tbx.Size = new System.Drawing.Size(69, 20);
            this.gmmCompNum_tbx.TabIndex = 43;
            // 
            // hmmStateNum_tbx
            // 
            this.hmmStateNum_tbx.Location = new System.Drawing.Point(106, 49);
            this.hmmStateNum_tbx.Name = "hmmStateNum_tbx";
            this.hmmStateNum_tbx.Size = new System.Drawing.Size(69, 20);
            this.hmmStateNum_tbx.TabIndex = 43;
            // 
            // dataType_cbx
            // 
            this.dataType_cbx.FormattingEnabled = true;
            this.dataType_cbx.Items.AddRange(new object[] {
            "MFCC- 13",
            "Delta MFCC- 26",
            "Double MFCC- 39"});
            this.dataType_cbx.Location = new System.Drawing.Point(72, 12);
            this.dataType_cbx.Name = "dataType_cbx";
            this.dataType_cbx.Size = new System.Drawing.Size(103, 21);
            this.dataType_cbx.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "HMM State Num:";
            // 
            // typeCep_lb
            // 
            this.typeCep_lb.AutoSize = true;
            this.typeCep_lb.Location = new System.Drawing.Point(8, 15);
            this.typeCep_lb.Name = "typeCep_lb";
            this.typeCep_lb.Size = new System.Drawing.Size(61, 13);
            this.typeCep_lb.TabIndex = 15;
            this.typeCep_lb.Text = "Type Ceps:";
            // 
            // apply_btn
            // 
            this.apply_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.apply_btn.Location = new System.Drawing.Point(221, 175);
            this.apply_btn.Name = "apply_btn";
            this.apply_btn.Size = new System.Drawing.Size(75, 23);
            this.apply_btn.TabIndex = 28;
            this.apply_btn.Text = "Apply";
            this.apply_btn.UseVisualStyleBackColor = true;
            this.apply_btn.Click += new System.EventHandler(this.apply_btn_Click);
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(307, 175);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(75, 23);
            this.ok_btn.TabIndex = 27;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // useStandardization_cbx
            // 
            this.useStandardization_cbx.AutoSize = true;
            this.useStandardization_cbx.Location = new System.Drawing.Point(32, 119);
            this.useStandardization_cbx.Name = "useStandardization_cbx";
            this.useStandardization_cbx.Size = new System.Drawing.Size(121, 17);
            this.useStandardization_cbx.TabIndex = 21;
            this.useStandardization_cbx.Text = "Use Standardization";
            this.useStandardization_cbx.UseVisualStyleBackColor = true;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 209);
            this.Controls.Add(this.apply_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.Mfcc);
            this.Name = "Setting";
            this.Text = "Setting";
            this.Mfcc.ResumeLayout(false);
            this.tabMfccSetting.ResumeLayout(false);
            this.tabMfccSetting.PerformLayout();
            this.tabPitch.ResumeLayout(false);
            this.tabPitch.PerformLayout();
            this.tabSetting.ResumeLayout(false);
            this.tabSetting.PerformLayout();
            this.tabLogSetting.ResumeLayout(false);
            this.tabLogSetting.PerformLayout();
            this.tabVad.ResumeLayout(false);
            this.tabVad.PerformLayout();
            this.tabTraining.ResumeLayout(false);
            this.tabTraining.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Mfcc;
        private System.Windows.Forms.TabPage tabMfccSetting;
        private System.Windows.Forms.TabPage tabPitch;
        private System.Windows.Forms.Label highfreq_hz_lb;
        private System.Windows.Forms.Label lowfreq_hz_lb;
        private System.Windows.Forms.Label timeshift_ms_lb;
        private System.Windows.Forms.Label timeframems_lb;
        private System.Windows.Forms.TextBox cepfilter_tb;
        private System.Windows.Forms.TextBox highfreq_tb;
        private System.Windows.Forms.TextBox numceps_tb;
        private System.Windows.Forms.TextBox lowfreq_tb;
        private System.Windows.Forms.Label cepfilter_lb;
        private System.Windows.Forms.TextBox timeshift_tb;
        private System.Windows.Forms.Label hightfreq_lb;
        private System.Windows.Forms.Label numceps_lb;
        private System.Windows.Forms.TextBox timeframe_tb;
        private System.Windows.Forms.Label lowfreq_lb;
        private System.Windows.Forms.Label timeshift_lb;
        private System.Windows.Forms.Label timeframe_lb;
        private System.Windows.Forms.ComboBox pitchtype_cbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox yinThreshold_tbx;
        private System.Windows.Forms.TextBox hightFreq_tbx;
        private System.Windows.Forms.TextBox lowFreq_tbx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox timeshift_tbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox timeframe_tbl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button apply_btn;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.TabPage tabSetting;
        private System.Windows.Forms.CheckBox normal_audio_cbx;
        private System.Windows.Forms.CheckBox remove_noise_cbx;
        private System.Windows.Forms.TabPage tabLogSetting;
        private System.Windows.Forms.CheckBox separate_log_cbx;
        private System.Windows.Forms.CheckBox enanblelog_cbx;
        private System.Windows.Forms.TabPage tabVad;
        private System.Windows.Forms.TextBox pitch_tbx;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox energy_txb;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox median_cb;
        private System.Windows.Forms.TextBox median_tbl;
        private System.Windows.Forms.Label window_lb;
        private System.Windows.Forms.CheckBox removeUnpitch_cb;
        private System.Windows.Forms.TabPage tabTraining;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox gmmCompNum_tbx;
        private System.Windows.Forms.TextBox hmmStateNum_tbx;
        private System.Windows.Forms.ComboBox dataType_cbx;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label typeCep_lb;
        private System.Windows.Forms.ComboBox gmmCoVarType_cbx;
        private System.Windows.Forms.ComboBox logLevel_cbx;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox shiftToZero_cbx;
        private System.Windows.Forms.CheckBox useStandardization_cbx;
    }
}