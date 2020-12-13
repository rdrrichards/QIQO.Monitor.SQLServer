/* eslint-disable @typescript-eslint/naming-convention */
export interface Exception {
  Message: string;
  InnerException?: Exception;
}

