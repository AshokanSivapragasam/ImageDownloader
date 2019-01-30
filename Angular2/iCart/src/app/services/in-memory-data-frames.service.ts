import { InMemoryDbService } from 'angular-in-memory-web-api';

export class InMemoryDataService implements InMemoryDbService {

  createDb() {
    const frameSections = [{
            id: 1,
            headline01 : 'ACUVUE LUXURY EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 3,
                    name : 'Acuvue1Daymoist30Pcs New Acuvue1Daymoist30Pcs New 38716',
                    imageUri : '~/../assets/Lenskart/acuvue1daymoist30pcs-new_acuvue1daymoist30pcs-new_38716.jpg',
                    imageCaption : 'Acuvue'
                }, {
                    id : 4,
                    name : 'Acuvueoasys New Acuvueoasys New 38712',
                    imageUri : '~/../assets/Lenskart/acuvueoasys-new_acuvueoasys-new_38712_1.jpg',
                    imageCaption : 'Acuvue'
                }
            ]
        }, {
            id: 2,
            headline01 : 'JOHN JACOBS PREMIUM EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 42,
                    name : 'John Jacobs E10302 C1 Eyeglasses J 5610',
                    imageUri : '~/../assets/Lenskart/john-jacobs-e10302-c1-eyeglasses_J_5610_1_1_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 43,
                    name : 'John Jacobs E10302 C4 Eyeglasses J 5626',
                    imageUri : '~/../assets/Lenskart/john-jacobs-e10302-c4-eyeglasses_j_5626_1_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 44,
                    name : 'John Jacobs E10302 C5 Eyeglasses J 5594',
                    imageUri : '~/../assets/Lenskart/john-jacobs-e10302-c5-eyeglasses_J_5594_1_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 45,
                    name : 'John Jacobs E10303 C2 Eyeglasses J 8980',
                    imageUri : '~/../assets/Lenskart/john-jacobs-e10303-c2-eyeglasses_J_8980_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 46,
                    name : 'John Jacobs Jj 2241 Rd Trt Orc4 Eyeglasses G 0022',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-2241-rd-trt-orc4-eyeglasses_g_0022_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 48,
                    name : 'John Jacobs Jj 2242 S C1 Eyeglasses Eyeglasses J819 2',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-2242-s-c1-eyeglasses_eyeglasses_J_1819_1_1_1_1_2_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 49,
                    name : 'John Jacobs Jj 4414 Matte Brown Wooden C5 Club Master Eyegl M636',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-4414-matte-brown-wooden-c5-club-master-eyegl_m_1636.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 51,
                    name : 'John Jacobs Jj 8035 Rd Trnt Ore C4 Eyeglasses G 0032',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-8035-rd-trnt-ore-c4-eyeglasses_g_0032_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 53,
                    name : 'John Jacobs Jj E10002 C5 Eyeglasses J480',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e10002-c5-eyeglasses_J_1480.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 55,
                    name : 'John Jacobs Jj E10229 C1 Eyeglasses John Jacobs Jj E10229 C1 Eyeglasses M 0072',
                    // tslint:disable-next-line:max-line-length
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e10229-c1-eyeglasses_john-jacobs-jj-e10229-c1-eyeglasses_M_0072_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 57,
                    name : 'John Jacobs Jj E10235 C3 Eyeglasses M 0001',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e10235-c3-eyeglasses_m_0001_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 58,
                    name : 'John Jacobs Jj E10267 C3 Eyeglasses J 3384',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e10267-c3-eyeglasses_j_3384_1_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 59,
                    name : 'John Jacobs Jj E10338 C2 Eyeglases M 5144',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e10338-c2-eyeglases_m_5144_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 60,
                    name : 'John Jacobs Jj E10528 C1 Eyeglasses Eyeglasses J638 2',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e10528-c1-eyeglasses_eyeglasses_j_1638_1_1_2_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 62,
                    name : 'John Jacobs Jj E10541 C1 Eyeglasses M 9060',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e10541-c1-eyeglasses_m_9060.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 63,
                    name : 'John Jacobs Jj E10541 C3 Eyeglasses J 0151',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e10541-c3-eyeglasses_j_0151_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 64,
                    name : 'John Jacobs Jj E11062 C1 Eyeglasses J 0822',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e11062-c1-eyeglasses_J_0822_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 65,
                    name : 'John Jacobs Jj E11464 C1 Eyeglasses G 9292',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e11464-c1-eyeglasses_g_9292.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 66,
                    name : 'John Jacobs Jj E11464 C2 Eyeglasses G 9312',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e11464-c2-eyeglasses_g_9312.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 67,
                    name : 'John Jacobs Jj E11831 C4 Full Rim Eyeglasses G 2159',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-e11831-c4-full-rim-eyeglasses_g_2159_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 68,
                    name : 'John Jacobs Jj S11122 C3 Sunglasses M239',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-s11122-c3-sunglasses_m_1239_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 69,
                    name : 'John Jacobs Jj S11122 C5 Sunglasses M251',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-s11122-c5-sunglasses_m_1251_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 70,
                    name : 'John Jacobs Jj S11528 C2 Sunglasses J 5958',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-s11528-c2-sunglasses_J_5958_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 71,
                    name : 'John Jacobs Jj S11528 C3 Sunglasses J 5980',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-s11528-c3-sunglasses_J_5980_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 72,
                    name : 'John Jacobs Jj S11528 C4 Sunglasses J 5936',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-s11528-c4-sunglasses_J_5936_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 73,
                    name : 'John Jacobs Jj S11528 C5 Sunglasses J 5947',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-s11528-c5-sunglasses_J_5947_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 74,
                    name : 'John Jacobs Jj Slim Jj 2241 Mat Blk Blu Sky Blu C2 Round Eye J966 3',
                    // tslint:disable-next-line:max-line-length
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-slim-jj-2241-mat-blk-blu-sky-blu-c2-round-eye_j_1966_1_1_1_3_1_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 75,
                    name : 'John Jacobs Jj T18520 C5 Full Rim Eyeglasses J 5506',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-t18520-c5-full-rim-eyeglasses_J_5506_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 76,
                    name : 'John Jacobs Jj T18522 C6 Full Rim Eyeglasses J 5545',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-t18522-c6-full-rim-eyeglasses_J_5545_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 77,
                    name : 'John Jacobs Jj T5379 C1 Full Rim Eyeglasses J 5521 3',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-t5379-c1-full-rim-eyeglasses_J_5521_3.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 78,
                    name : 'John Jacobs Jj T5379 C2 Full Rim Eyeglasses J 5567 3',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-t5379-c2-full-rim-eyeglasses_J_5567_3.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 79,
                    name : 'John Jacobs Jj T5379 C3 Full Rim Eyeglasses J 5534 3',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-t5379-c3-full-rim-eyeglasses_J_5534_3.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 80,
                    name : 'John Jacobs Jj T5379 C7 Full Rim Eyeglasses J 5578',
                    imageUri : '~/../assets/Lenskart/john-jacobs-jj-t5379-c7-full-rim-eyeglasses_J_5578_1.jpg',
                    imageCaption : 'John Jacobs'
                }, {
                    id : 81,
                    name : 'John Jacobse10303 C1 Eyeglasses J 9000',
                    imageUri : '~/../assets/Lenskart/john-jacobse10303-c1-eyeglasses_J_9000_1.jpg',
                    imageCaption : 'John Jacobs'
                }
            ]
        }, {
            id: 3,
            headline01 : 'MASK PREMIUM EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 97,
                    name : 'Mask Ms E10753 C1 Eyeglasses M 7693',
                    imageUri : '~/../assets/Lenskart/mask-ms-e10753-c1-eyeglasses_m_7693_1_1.jpg',
                    imageCaption : 'Mask'
                }, {
                    id : 98,
                    name : 'Mask Ms E10753 C2 Eyeglasses M 7568',
                    imageUri : '~/../assets/Lenskart/mask-ms-e10753-c2-eyeglasses_m_7568_1_1_1.jpg',
                    imageCaption : 'Mask'
                }, {
                    id : 99,
                    name : 'Mask Ms E10753 C3 Eyeglasses M 7652',
                    imageUri : '~/../assets/Lenskart/mask-ms-e10753-c3-eyeglasses_m_7652_1_1_1.jpg',
                    imageCaption : 'Mask'
                }, {
                    id : 100,
                    name : 'Mask Ms E10755 C1 Eyeglasses M 7541',
                    imageUri : '~/../assets/Lenskart/mask-ms-e10755-c1-eyeglasses_m_7541_1_1.jpg',
                    imageCaption : 'Mask'
                }, {
                    id : 101,
                    name : 'Mask Ms E10757 C1 Eyeglasses M 7735',
                    imageUri : '~/../assets/Lenskart/mask-ms-e10757-c1-eyeglasses_m_7735_1_1.jpg',
                    imageCaption : 'Mask'
                }, {
                    id : 102,
                    name : 'Mask Ms E10758 C3 Eyeglasses M 7709 2',
                    imageUri : '~/../assets/Lenskart/mask-ms-e10758-c3-eyeglasses_m_7709_1_2.jpg',
                    imageCaption : 'Mask'
                }
            ]
        }, {
            id: 4,
            headline01 : 'OAKLEY LUXURY EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 88,
                    name : 'Lux Oakley Ox1100 Size 51 01 Eyeglasses M 7561',
                    imageUri : '~/../assets/Lenskart/lux-oakley-ox1100-size-51-01-eyeglasses_m_7561.jpg',
                    imageCaption : 'Oakley'
                }
            ]
        }, {
            id: 5,
            headline01 : 'OAKLEY PREMIUM EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 106,
                    name : 'Oakley Ox3129 Size 53 07 Eyeglasses Oakley Ox3129 Size 53 07 Eyeglasses M 0464 9',
                    // tslint:disable-next-line:max-line-length
                    imageUri : '~/../assets/Lenskart/oakley-ox3129-size-53-07-eyeglasses_oakley-ox3129-size-53-07-eyeglasses_m_0464_9.jpg',
                    imageCaption : 'Oakley'
                }, {
                    id : 107,
                    name : 'Oakley Ox8039 01 Size 53 Eyeglasses D 9680 3',
                    imageUri : '~/../assets/Lenskart/oakley-ox8039-01-size-53-eyeglasses_d_9680_3_1_1.jpg',
                    imageCaption : 'Oakley'
                }, {
                    id : 108,
                    name : 'Oakley Ox8049 01 Size 53 Eyeglasses Oakley Ox8049 01 Size 53 Eyeglasses  M 0299 6',
                    // tslint:disable-next-line:max-line-length
                    imageUri : '~/../assets/Lenskart/oakley-ox8049-01-size-53-eyeglasses_oakley-ox8049-01-size-53-eyeglasses__m_0299_6.jpg',
                    imageCaption : 'Oakley'
                }, {
                    id : 109,
                    name : 'Oakley Youth Oy8008 03 Size 50 Kids Eyeglasses G 9466',
                    imageUri : '~/../assets/Lenskart/oakley-youth-oy8008-03-size-50-kids-eyeglasses_g_9466.jpg',
                    imageCaption : 'Oakley'
                }, {
                    id : 110,
                    name : 'Oakley Youth Oy8008 04 Size 50 Kids Eyeglasses G 9604',
                    imageUri : '~/../assets/Lenskart/oakley-youth-oy8008-04-size-50-kids-eyeglasses_g_9604.jpg',
                    imageCaption : 'Oakley'
                }
            ]
        }, {
            id: 6,
            headline01 : 'RAY-BAN LUXURY EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 89,
                    name : 'Lux Ray Ban Rb3025 003 3F Size 58 Aviator Men Metal Sunglass M 7765',
                    imageUri : '~/../assets/Lenskart/lux-ray-ban-rb3025-003-3f-size-58-aviator-men-metal-sunglass_m_7765_1_1.jpg',
                    imageCaption : 'RAY-BAN'
                }, {
                    id : 91,
                    name : 'Lux Ray Ban Rb3025 L0205 Size 58 Golden Sunglasses J 9909',
                    imageUri : '~/../assets/Lenskart/lux-ray-ban-rb3025-l0205-size-58-golden-sunglasses_j_9909_1_1.jpg',
                    imageCaption : 'RAY-BAN'
                }, {
                    id : 93,
                    name : 'Lux Ray Ban Rb3025I L2823 Size 58 Men Metal Sunglasses M111',
                    imageUri : '~/../assets/Lenskart/lux-ray-ban-rb3025i-l2823-size-58-men-metal-sunglasses_m_1111_1.jpg',
                    imageCaption : 'RAY-BAN'
                }, {
                    id : 94,
                    name : 'Lux Ray Ban Rb3026 W2027 Size 62 Men Steel Sunglasses J 9874',
                    imageUri : '~/../assets/Lenskart/lux-ray-ban-rb3026-w2027-size-62-men-steel-sunglasses_j_9874_1_1_1.jpg',
                    imageCaption : 'RAY-BAN'
                }, {
                    id : 95,
                    name : 'Lux Rayban Rb3025 001 58 Polarized Size 58 Sunglasses Sunglasses J 0014',
                    imageUri : '~/../assets/Lenskart/lux-rayban-rb3025-001-58-polarized-size-58-sunglasses_sunglasses_j_0014_1_1.jpg',
                    imageCaption : 'RAY-BAN'
                }, {
                    id : 96,
                    name : 'Lux Rb3025 002 58 Size Size M 0873',
                    imageUri : '~/../assets/Lenskart/lux-rb3025-002-58-size_size_m_0873_1_1_1.jpg',
                    imageCaption : 'RAY-BAN'
                }
            ]
        }, {
            id: 7,
            headline01 : 'RAY-BAN PREMIUM EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 120,
                    name : 'Ray Ban Rb3025 9065V7 Size 58 Sunglasses G 2391',
                    imageUri : '~/../assets/Lenskart/ray-ban-rb3025-9065v7-size-58-sunglasses_g_2391_1.jpg',
                    imageCaption : 'Ray-ban'
                }, {
                    id : 121,
                    name : 'Ray Ban Rb3025 90664A Size 58 Sunglasses G 0367',
                    imageUri : '~/../assets/Lenskart/ray-ban-rb3025-90664a-size-58-sunglasses_g_0367_1_1_1.jpg',
                    imageCaption : 'Ray-ban'
                }, {
                    id : 122,
                    name : 'Ray Ban Rb4252 7103 Size 51 Sunglasses M 7672',
                    imageUri : '~/../assets/Lenskart/ray-ban-rb4252-710-13-size-51-sunglasses_m_7672_1_1.jpg',
                    imageCaption : 'Ray-ban'
                }, {
                    id : 123,
                    name : 'Ray Ban Rb4252 710 71 Size 51 Sunglasses M870',
                    imageUri : '~/../assets/Lenskart/ray-ban-rb4252-710-71-size-51-sunglasses_M_1870_1_1_1.jpg',
                    imageCaption : 'Ray-ban'
                }, {
                    id : 124,
                    name : 'Ray Ban Rx5337 Size 52 5528 Eyeglasses Ray Ban Rx5337 Size 52 5528 Eyeglasses M 9963',
                    // tslint:disable-next-line:max-line-length
                    imageUri : '~/../assets/Lenskart/ray-ban-rx5337-size-52-5528-eyeglasses_ray-ban-rx5337-size-52-5528-eyeglasses_m_9963.jpg',
                    imageCaption : 'Ray-ban'
                }, {
                    id : 125,
                    name : 'Ray Ban Rx6303 Size 53 2502 Eyeglasses M 9848',
                    imageUri : '~/../assets/Lenskart/ray-ban-rx6303-size-53-2502-eyeglasses_m_9848_1_1_1.jpg',
                    imageCaption : 'Ray-ban'
                }, {
                    id : 126,
                    name : 'Ray Ban Rx7027 2000 Size 54 Eyeglasses M 7835',
                    imageUri : '~/../assets/Lenskart/ray-ban-rx7027-2000-size-54-eyeglasses_m_7835.jpg',
                    imageCaption : 'Ray-ban'
                }, {
                    id : 127,
                    name : 'Ray Ban Rx7091 Size 54 2000 Eyeglasses Ray Ban Rx7091 Size 54 2000 Eyeglasses M 4421',
                    // tslint:disable-next-line:max-line-length
                    imageUri : '~/../assets/Lenskart/ray-ban-rx7091-size-54-2000-eyeglasses_ray-ban-rx7091-size-54-2000-eyeglasses_m_4421.jpg',
                    imageCaption : 'Ray-ban'
                }, {
                    id : 128,
                    name : 'Ray Ban Youth Ry1054 4073 Size 49 Kids Eyeglasses G 0933',
                    imageUri : '~/../assets/Lenskart/ray-ban-youth-ry1054-4073-size-49-kids-eyeglasses_g_0933_1.jpg',
                    imageCaption : 'Ray-ban'
                }, {
                    id : 129,
                    name : 'Ray Ban Youth Ry1586 3777 Size 47 Kids Eyeglasses G 9595',
                    imageUri : '~/../assets/Lenskart/ray-ban-youth-ry1586-3777-size-47-kids-eyeglasses_g_9595.jpg',
                    imageCaption : 'Ray-ban'
                }, {
                    id : 130,
                    name : 'Ray Ban Youth Ry1586 3777 Size 49 Kids Eyeglasses G 9604',
                    imageUri : '~/../assets/Lenskart/ray-ban-youth-ry1586-3777-size-49-kids-eyeglasses_g_9604_1.jpg',
                    imageCaption : 'Ray-ban'
                }
            ]
        }, {
            id: 8,
            headline01 : 'SUNPOCKET PREMIUM EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 137,
                    name : 'Sunpocket Kauai Pale Blue Id222 Sunglasses M 8990',
                    imageUri : '~/../assets/Lenskart/sunpocket-kauai-pale-blue-id-1222-sunglasses_M_8990_1.jpg',
                    imageCaption : 'Sun Pocket'
                }, {
                    id : 138,
                    name : 'Sunpocket Sunpocket Ii White Sea Id237 Sunglasses M 8973',
                    imageUri : '~/../assets/Lenskart/sunpocket-sunpocket-ii-white-sea-id-1237-sunglasses_M_8973_1.jpg',
                    imageCaption : 'Sun Pocket'
                }
            ]
        }, {
            id: 9,
            headline01 : 'THINOPTICS PREMIUM EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 139,
                    name : 'Thinoptics Reading Gla With Case Black Only For50 Pwr Eye50 Pwr Eyest Image 2',
                    // tslint:disable-next-line:max-line-length
                    imageUri : '~/../assets/Lenskart/thinoptics-reading-gla-with-case-black-only-for-150-pwr-eye_150-pwr-eye_1st_image_2.jpg',
                    imageCaption : 'Thin Optics'
                }, {
                    id : 140,
                    name : 'Thinoptics Reading Gla With Case Black Only For 200 Pwr Eye 200 Pwr Eyest Image 2',
                    // tslint:disable-next-line:max-line-length
                    imageUri : '~/../assets/Lenskart/thinoptics-reading-gla-with-case-black-only-for-200-pwr-eye_200-pwr-eye_1st_image_2.jpg',
                    imageCaption : 'Thin Optics'
                }, {
                    id : 141,
                    name : 'Thinoptics Reading Gla With Case Black Only For 250 Pwr Eye 250 Pwr Eyest Image 2',
                    // tslint:disable-next-line:max-line-length
                    imageUri : '~/../assets/Lenskart/thinoptics-reading-gla-with-case-black-only-for-250-pwr-eye_250-pwr-eye_1st_image_2_1.jpg',
                    imageCaption : 'Thin Optics'
                }
            ]
        }, {
            id: 10,
            headline01 : 'VINCENT CHASE PREMIUM EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 154,
                    name : 'Vincent Chase Crayons Vc E11393 C4 Eyeglasses G 8594',
                    imageUri : '~/../assets/Lenskart/vincent-chase-crayons-vc-e11393-c4-eyeglasses_g_8594.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 155,
                    name : 'Vincent Chase Gucrayons Vc E11393 C2 Eyeglasses G 8611',
                    imageUri : '~/../assets/Lenskart/vincent-chase-gucrayons-vc-e11393-c2-eyeglasses_g_8611.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 156,
                    name : 'Vincent Chase Jj 2239 Full Rim Cat Eye C7 Eyeglasses G957',
                    imageUri : '~/../assets/Lenskart/vincent-chase-jj-2239-full-rim-cat-eye-c7-eyeglasses_g_1957.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 157,
                    name : 'Vincent Chase Matte Gu Crayons Vc E11393 C3 Eyeglasses G 8562',
                    imageUri : '~/../assets/Lenskart/vincent-chase-matte-gu-crayons-vc-e11393-c3-eyeglasses_g_8562.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 158,
                    name : 'Vincent Chase Polarized Vc S10659 C5 Sunglasses G 8737',
                    imageUri : '~/../assets/Lenskart/vincent-chase-polarized-vc-s10659-c5-sunglasses_g_8737_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 159,
                    name : 'Vincent Chase Polarized Vc S10659 C6 Sunglasses G 8761',
                    imageUri : '~/../assets/Lenskart/vincent-chase-polarized-vc-s10659-c6-sunglasses_g_8761_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 160,
                    name : 'Vincent Chase Polarized Vc S11075 C7 Sunglasses G 4934',
                    imageUri : '~/../assets/Lenskart/vincent-chase-polarized-vc-s11075-c7-sunglasses_g_4934_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 161,
                    name : 'Vincent Chase Rectangle Vc E10027 C4 Eyeglasses G738',
                    imageUri : '~/../assets/Lenskart/vincent-chase-rectangle-vc-e10027-c4-eyeglasses_g_1738.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 162,
                    name : 'Vincent Chase Rectangle Vc E10483 C8 Eyeglasses G 0050',
                    imageUri : '~/../assets/Lenskart/vincent-chase-rectangle-vc-e10483-c8-eyeglasses_g_0050.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 163,
                    name : 'Vincent Chase Size Zero Vc 5881 Blue C1 Eyeglasses M 3465',
                    imageUri : '~/../assets/Lenskart/vincent-chase-size-zero-vc-5881-blue-c1-eyeglasses_m_3465_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 164,
                    name : 'Vincent Chase Size Zero Vc 5882 Gunmetal Black C2 Eyeglasses G 0058',
                    imageUri : '~/../assets/Lenskart/vincent-chase-size-zero-vc-5882-gunmetal-black-c2-eyeglasses_g_0058.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 165,
                    name : 'Vincent Chase Vc 0316 Black010 Eyeglasses J 2290',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-0316-black-1010-eyeglasses_j_2290_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 166,
                    name : 'Vincent Chase Vc 5030 C13 Sunglasses J 9062 2',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5030-c13-sunglasses_j_9062_1_2_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 167,
                    name : 'Vincent Chase Vc 5030 C14 Sunglasses J 9046 2',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5030-c14-sunglasses_j_9046_1_2_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 168,
                    name : 'Vincent Chase Vc 5147 C101 Sunglasses M 4017 4',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5147-c101-sunglasses_m_4017_1_1_4_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 169,
                    name : 'Vincent Chase Vc 5158 Black Blue Mirror120 U1 Aviator Sun M 3888 3',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5158-black-blue-mirror-1120-u1-aviator-sun_m_3888_1_1_3_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 170,
                    name : 'Vincent Chase Vc 5158 Black Yellow Mirror130 V1Aviator Sun M 3909 3',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5158-black-yellow-mirror-1130-v1aviator-sun_m_3909_1_3_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 171,
                    name : 'Vincent Chase Vc 5158 Blk Gldn Mir Avtor C8 Sunglasses M 7224 2U',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5158-blk-gldn-mir-avtor-c8-sunglasses_m_7224_2u_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 172,
                    name : 'Vincent Chase Vc 5158 C 89 Sunglasses G 5301',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5158-c-89-sunglasses_g_5301_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 173,
                    name : 'Vincent Chase Vc 5158 C83 Sunglasses M 3517 3',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5158-c83-sunglasses_m_3517_3_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 174,
                    name : 'Vincent Chase Vc 5158 Golden Brown C43 Aviator Sunglasses M 3573 3',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5158-golden-brown-c43-aviator-sunglasses_m_3573_1_3_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 176,
                    name : 'Vincent Chase Vc 5158 Green Grey Gradient111 Vo Avitor Sun M 4992 2',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5158-green-grey-gradient-1111-vo-avitor-sun_m_4992_1_2_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 177,
                    name : 'Vincent Chase Vc 5158 P Black Grey Gradient C9 Aviator Sun J 5510 2 4',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5158-p-black-grey-gradient-c9-aviator-sun_j_5510_2_4_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 179,
                    name : 'Vincent Chase Vc 5158 P C20 Sunglasses Sunglasses 4 3',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5158-p-c20-sunglasses_sunglasses_1_1_1_4_3_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 180,
                    name : 'Vincent Chase Vc 5186 P Matte Blk Ble Gradient C2 Wayfr Sun J 3444',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-5186-p-matte-blk-ble-gradient-c2-wayfr-sun_j_3444.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 181,
                    name : 'Vincent Chase Vc E10027 C6 Eyeglasses J 0654 2',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10027-c6-eyeglasses_J_0654_2_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 182,
                    name : 'Vincent Chase Vc E10102 Brown C23 Eyeglasses G 9334',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10102-brown-c23-eyeglasses_g_9334.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 183,
                    name : 'Vincent Chase Vc E10211 C6 Eyeglasses M 8665',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10211-c6-eyeglasses_m_8665_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 184,
                    name : 'Vincent Chase Vc E10213 Mte Blk Rd C6 Eyeglasses G804',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10213-mte-blk-rd-c6-eyeglasses_g_1804.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 186,
                    name : 'Vincent Chase Vc E10436 C2 Eyeglasses Vincent Chase Vc E10436 C2 Eyeglasses M 5318',
                    // tslint:disable-next-line:max-line-length
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10436-c2-eyeglasses_vincent-chase-vc-e10436-c2-eyeglasses_m_5318.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 187,
                    name : 'Vincent Chase Vc E10481 C5 Eyeglasses M 9225',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10481-c5-eyeglasses_m_9225_1_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 188,
                    name : 'Vincent Chase Vc E10592 C2 Eyeglasses J 0081',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10592-c2-eyeglasses_j_0081_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 190,
                    name : 'Vincent Chase Vc E10598 M C1 Eyeglasses M 9533',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10598-m-c1-eyeglasses_m_9533_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 192,
                    name : 'Vincent Chase Vc E10620 C1 Eyeglasses J 0803',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10620-c1-eyeglasses_j_0803_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 193,
                    name : 'Vincent Chase Vc E10775 M Full Rim Aviator C5 Eyeglasses G 2342',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10775-m-full-rim-aviator-c5-eyeglasses_g_2342.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 194,
                    name : 'Vincent Chase Vc E10775 S Full Rim Aviator C5 Eyeglasses G 2334',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10775-s-full-rim-aviator-c5-eyeglasses_g_2334.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 195,
                    name : 'Vincent Chase Vc E10858 C1 Eyeglasses Es J 3606',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e10858-c1-eyeglasses_es_J_3606_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 196,
                    name : 'Vincent Chase Vc E11003 M C1 Eyeglasses M 2040',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11003-m-c1-eyeglasses_m_2040_1_1_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 197,
                    name : 'Vincent Chase Vc E11003 M C2 Eyeglasses M 2024',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11003-m-c2-eyeglasses_m_2024_1_1_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 198,
                    name : 'Vincent Chase Vc E11003 M C4 Eyeglasses M 2153 ',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11003-m-c4-eyeglasses_m_2153_1_1_1_.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 199,
                    name : 'Vincent Chase Vc E11003 S C3 Eyeglasses M 2137',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11003-s-c3-eyeglasses_m_2137_1_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 200,
                    name : 'Vincent Chase Vc E11003 S C4 Eyeglasses M 2169',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11003-s-c4-eyeglasses_m_2169_1_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 201,
                    name : 'Vincent Chase Vc E11068 C1 Eyeglasses M 2353',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11068-c1-eyeglasses_m_2353_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 202,
                    name : 'Vincent Chase Vc E11068 C2 Eyeglasses M 2345',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11068-c2-eyeglasses_m_2345_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 203,
                    name : 'Vincent Chase Vc E11156 C2 Eyeglasses M 6499',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11156-c2-eyeglasses_m_6499_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 204,
                    name : 'Vincent Chase Vc E11158 C3 Eyeglasses M 6539',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11158-c3-eyeglasses_m_6539_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 205,
                    name : 'Vincent Chase Vc E11195 C3 Eyeglasses M 2430',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11195-c3-eyeglasses_m_2430_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 206,
                    name : 'Vincent Chase Vc E11196 C1 Eyeglasses M 2378',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11196-c1-eyeglasses_m_2378_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 207,
                    name : 'Vincent Chase Vc E11197 C1 Eyeglasses M 2413',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11197-c1-eyeglasses_m_2413_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 208,
                    name : 'Vincent Chase Vc E11197 C2 Eyeglasses M 2386',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11197-c2-eyeglasses_m_2386_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 210,
                    name : 'Vincent Chase Vc E11267 Brown Blue Tortoise C3 Eyeglasses G 9179',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11267-brown-blue-tortoise-c3-eyeglasses_g_9179.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 211,
                    name : 'Vincent Chase Vc E11273 C3 Eyeglasses M414',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11273-c3-eyeglasses_m_1414_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 212,
                    name : 'Vincent Chase Vc E11275 C2 Eyeglasses M454',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11275-c2-eyeglasses_m_1454_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 213,
                    name : 'Vincent Chase Vc E11276 C1 Eyeglasses M446',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11276-c1-eyeglasses_m_1446_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 214,
                    name : 'Vincent Chase Vc E11276 C2 Eyeglasses M422',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11276-c2-eyeglasses_m_1422_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 215,
                    name : 'Vincent Chase Vc E11276 C3 Eyeglasses M382',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11276-c3-eyeglasses_m_1382_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 216,
                    name : 'Vincent Chase Vc E11276 C4 Eyeglasses M398',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11276-c4-eyeglasses_m_1398_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 217,
                    name : 'Vincent Chase Vc E11283 C2 Eyeglasses Eyeglasses M 2633',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11283-c2-eyeglasses_eyeglasses_m_2633.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 218,
                    name : 'Vincent Chase Vc E11288 C1 Eyeglasses M 2522',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11288-c1-eyeglasses_m_2522_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 219,
                    name : 'Vincent Chase Vc E11288 C2 Eyeglasses M 2749',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11288-c2-eyeglasses_m_2749_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 221,
                    name : 'Vincent Chase Vc E11289 C2 Eyeglasses M 2704',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11289-c2-eyeglasses_m_2704_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 222,
                    name : 'Vincent Chase Vc E11335 C4 B Rimless Eyeglasses M 3286',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11335-c4-b-rimless-eyeglasses_m_3286_1_1_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 223,
                    name : 'Vincent Chase Vc E11369 C2 Eyeglasses M 5358 2',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11369-c2-eyeglasses_m_5358_2_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 224,
                    name : 'Vincent Chase Vc E11380 C1 Eyeglasses M 5618 2',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11380-c1-eyeglasses_m_5618_2_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 225,
                    name : 'Vincent Chase Vc E11385 C4 Eyeglasses J680',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11385-c4-eyeglasses_j_1680_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 226,
                    name : 'Vincent Chase Vc E11392 C3 Eyeglasses G 2208',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11392-c3-eyeglasses_g_2208_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 227,
                    name : 'Vincent Chase Vc E11394 C3 Eyeglasses G 2049',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11394-c3-eyeglasses_g_2049_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 229,
                    name : 'Vincent Chase Vc E11395 C2 Eyeglasses G 2131',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11395-c2-eyeglasses_g_2131_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 230,
                    name : 'Vincent Chase Vc E11395 C3 Eyeglasses G 2200',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11395-c3-eyeglasses_g_2200_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 231,
                    name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8567.jpg',
                    imageCaption : 'Vincent Chase',
                    framesIn360View: [{
                        id : 1,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e10491-silver-yellow-gry-c5-eye_mediumeye_1.jpg',
                        // tslint:disable-next-line:max-line-length
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e10491-silver-yellow-gry-c5-eye_mediumeye_1.jpg'
                    }, {
                        id : 2,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        // tslint:disable-next-line:max-line-length
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e10491-silver-yellow-gry-c5-eye_VC_Eye_Case_1.jpg',
                        // tslint:disable-next-line:max-line-length
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e10491-silver-yellow-gry-c5-eye_VC_Eye_Case_1.jpg'
                    }, {
                        id : 3,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_f_2192.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_f_2192.jpg'
                    }, {
                        id : 4,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8567.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8567.jpg'
                    }, {
                        id : 5,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8569.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8569.jpg'
                    }, {
                        id : 6,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8569_image_pla.jpg',
                        // tslint:disable-next-line:max-line-length
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8569_image_pla.jpg'
                    }, {
                        id : 8,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8571.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8571.jpg'
                    }, {
                        id : 9,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8572.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8572.jpg'
                    }, {
                        id : 10,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8574.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8574.jpg'
                    }, {
                        id : 6,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8569_image_pla.jpg',
                        // tslint:disable-next-line:max-line-length
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8569_image_pla.jpg'
                    }, {
                        id : 8,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8571.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8571.jpg'
                    }, {
                        id : 9,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8572.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8572.jpg'
                    }, {
                        id : 10,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8574.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8574.jpg'
                    }, {
                        id : 6,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8569_image_pla.jpg',
                        // tslint:disable-next-line:max-line-length
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8569_image_pla.jpg'
                    }, {
                        id : 8,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8571.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8571.jpg'
                    }, {
                        id : 9,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8572.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8572.jpg'
                    }, {
                        id : 10,
                        name : 'Vincent Chase Vc E11395 Half Rim Rct C4 Eye G 8567',
                        productCode : 'G 8567',
                        viewOrPerspective : 'Frontal',
                        imageUri : '~/../assets/Lenskart/11395/Images/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8574.jpg',
                        thumbnailUri : '~/../assets/Lenskart/11395/Thumbnails/vincent-chase-vc-e11395-half-rim-rct-c4-eye_g_8574.jpg'
                    }]
                }, {
                    id : 232,
                    name : 'Vincent Chase Vc E11400 C3 Eyeglasses G 2148',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11400-c3-eyeglasses_g_2148_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 233,
                    name : 'Vincent Chase Vc E11401 C2 Eyeglasses G 2023',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11401-c2-eyeglasses_g_2023_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 234,
                    name : 'Vincent Chase Vc E11402 C1 Eyeglasses J 0017',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11402-c1-eyeglasses_j_0017.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 236,
                    name : 'Vincent Chase Vc E11558 C1 Eyeglasses J 5796',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11558-c1-eyeglasses_J_5796_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 237,
                    name : 'Vincent Chase Vc E11578 C1 Eyeglasses J 5654',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11578-c1-eyeglasses_j_5654.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 238,
                    name : 'Vincent Chase Vc E11578 C2 Eyeglasses J 5694',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11578-c2-eyeglasses_j_5694.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 239,
                    name : 'Vincent Chase Vc E11578 C3 Eyeglasses J 5718',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11578-c3-eyeglasses_j_5718.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 240,
                    name : 'Vincent Chase Vc E11579 C2 Eyeglasses J 5621',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11579-c2-eyeglasses_j_5621.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 241,
                    name : 'Vincent Chase Vc E11583 C3 Eyeglasses J 0038',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-e11583-c3-eyeglasses_j_0038_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 242,
                    name : 'Vincent Chase Vc S10063 C1 Sunglasses J 8607',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-s10063-c1-sunglasses_j_8607_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 243,
                    name : 'Vincent Chase Vc S10082 C2 Sunglasses M377 3',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-s10082-c2-sunglasses_m_1377_1_1_3_1_1_1.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 244,
                    name : 'Vincent Chase Vc S11211 C4 Sunglasses J 5911',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-s11211-c4-sunglasses_j_5911.jpg',
                    imageCaption : 'Vincent Chase'
                }, {
                    id : 245,
                    name : 'Vincent Chase Vc S11320 C6 Sunglasses J 5206',
                    imageUri : '~/../assets/Lenskart/vincent-chase-vc-s11320-c6-sunglasses_j_5206_1.jpg',
                    imageCaption : 'Vincent Chase'
                }
            ]
        }, {
            id: 11,
            headline01 : 'VOGUE PREMIUM EYEGLASSES',
            headline02 : 'Ultra Light | Corrosion Free | Best in Class Material | 1 Year Warranty',
            headline03 : 'A great collection of 600+ Eyeglasses | Buy 1 Get 1 Free With Lenskart Gold Membership',
            frames : [{
                    id : 246,
                    name : 'Vogue Vo2886 W44 Size 51 Womens Eyeglasses M 2362',
                    imageUri : '~/../assets/Lenskart/vogue-vo2886-w44-size-51-womens-eyeglasses_M_2362.jpg',
                    imageCaption : 'Vogue'
                }, {
                    id : 247,
                    name : 'Vogue Vo2937 Size 51 W44 Eyeglasses M 4276 4',
                    imageUri : '~/../assets/Lenskart/vogue-vo2937-size-51-w44-eyeglasses_m_4276_4_1_1_1.jpg',
                    imageCaption : 'Vogue'
                }, {
                    id : 248,
                    name : 'Vogue Vo5223 50 2636 Eyeglasses G 0907',
                    imageUri : '~/../assets/Lenskart/vogue-vo5223-50-2636-eyeglasses_g_0907.jpg',
                    imageCaption : 'Vogue'
                }, {
                    id : 249,
                    name : 'Vogue Vo5225 53 2386 Eyeglasses G 0981',
                    imageUri : '~/../assets/Lenskart/vogue-vo5225-53-2386-eyeglasses_g_0981_1.jpg',
                    imageCaption : 'Vogue'
                }
            ]
        }
    ];

      return { frameSections };
  }
}
