export interface KuralsModel {
    chapter: string;
    kural: string[];
    number: number;
    section: string;
    meaning: KuralMeaningModel;
}

export interface KuralMeaningModel {
    ta_mu_va: string;
    ta_salamon: string;
    en: string;
}

export interface ThirukkuralModel {
    chapters: string[];
    kurals: KuralsModel[];
    sections: string[];
}
