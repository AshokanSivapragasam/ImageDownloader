import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { Disc } from '../../common/disc';
import { Album } from '../../common/album';
import { MusicZunePlayerPage } from '../music-zune-player/music-zune-player';
import { FileTransfer, FileTransferObject } from '@ionic-native/file-transfer';
import { File } from '@ionic-native/file';
import { FilePath } from '@ionic-native/file-path';
import { FileTransferMock, FileTransferObjectMock } from '@ionic-native-mocks/file-transfer';
import { FileMock, Entry } from '@ionic-native-mocks/file';
import { FilePathMock } from '@ionic-native-mocks/file-path';
import { Platform } from 'ionic-angular';
import { resolveDep } from '@angular/core/src/view/provider';
import './customized_mocks';

/**
 * Generated class for the MusicZuneHomePage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@Component({
  selector: 'music-zune-home',
  templateUrl: 'music-zune-home.html',
  providers: [ { provide: FilePath, useClass: FilePathMock },
               { provide: File, useClass: FileMock },
               { provide: FileTransfer, useClass: FileTransferMock },
               { provide: FileTransferObject, useClass: FileTransferObjectMock }]
})

export class MusicZuneHomePage {
  albums: Array<Album> = [];
  discs001: Array<Disc> = [];
  discs002: Array<Disc> = [];

  constructor(public navCtrl: NavController,
              public navParams: NavParams,
              public file: File,
              public filePath: FilePath,
              public transfer: FileTransfer,
              public transferObject: FileTransferObject,
              public platform: Platform) {
    this.platform.ready().then(()=> {

      file.listDir(file.externalDataDirectory, '/read/this/dir').then((files) => {
        for (let _file of files) {
          console.info(`${_file.name} | ${_file.nativeURL} | ${_file.fullPath}`);
        }
      });

      for(let idx = 1; idx < 50; idx += 1) {
        this.albums.push({
          id: idx,
          title: 'Album :: ' + idx,
          albumPictureUri: '../../assets/imgs/surya.jpg',
          discs: this.createDiscs(idx)
        });
      }
    });
  }

  createDiscs(maxDiscs: number): Array<Disc> {
    let discs: Array<Disc> = [];
    for(let idx = 0; idx < maxDiscs; idx += 1) {
      discs.push({
        id: idx,
        title: 'Disc - ' + idx,
        mediaUri: 'http://localhost/vault/audio/Munbe_vaa_theme.mp3',
        albumPictureUri: '../../assets/imgs/surya.jpg',
        playState: 'stopped'
      });
    }

    return discs;
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad MusicZuneHomePage');
  }

  openAlbum(album: Album): void {
    this.navCtrl.push(MusicZunePlayerPage, { 
      selectedAlbum: album
    });
  }
}
