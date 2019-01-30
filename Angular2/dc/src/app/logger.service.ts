import { Injectable } from '@angular/core';

@Injectable()
export class LoggerService {
  messages: string[] = [];
  constructor() { }

  info(message: string): void {
    // this.messages.push(message);
    console.log(message);
  }

  error(message: string): void {
    // this.messages.push(message);
    console.error(message);
  }
}
