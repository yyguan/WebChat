import { Pipe, PipeTransform } from "@angular/core";
import { Observable } from "rxjs";

import { Response } from "@angular/http"

@Pipe({ name: 'keys' })
export class KeysPipe implements PipeTransform {
    transform(value: any, args: any[] = null): string[] {
        if (typeof value === "object") {
            return Object.keys(value);
        } else {
            return emptyArray;
        }
    }
}
@Pipe({ name: 'values', pure: false })
export class ValuesPipe implements PipeTransform {
    transform(value: any, args: any[] = null): any {
        if (typeof value === "object") {
            return Object.keys(value).map(key => value[key]);
        } else {
            return emptyArray;
        }
    }
}
/** object to Array<{key:string,value:any}>*/
@Pipe({ name: 'object', pure: false })
export class ObjectPipe implements PipeTransform {
    transform(value: object, args: any[] = null): any {
        if (typeof value === "object") {
            return Object.keys(value).map(k => {
                return {
                    key: k,
                    value: value[k]
                };
            });
        } else {
            return emptyArray;
        }
    }
}

const emptyArray = [];

export class QueryStringBuilder {
    static BuildParametersFromSearch<T>(obj: T): URLSearchParams {
        let params: URLSearchParams = new URLSearchParams();

        if (obj == null)
        {
            return params;
        }

        QueryStringBuilder.PopulateSearchParams(params, '', obj);

        return params;
    }

    private static PopulateArray<T>(params: URLSearchParams, prefix: string, val: Array<T>) {
        for (let index in val) {
            let key = prefix + '[' + index + ']';
            let value: any = val[index];
            QueryStringBuilder.PopulateSearchParams(params, key, value);
        }
    }

    private static PopulateObject<T>(params: URLSearchParams, prefix: string, val: T) {
        const objectKeys = Object.keys(val) as Array<keyof T>;

        if (prefix) {
            prefix = prefix + '.';
        }

        for (let objKey of objectKeys) {

            let value = val[objKey];
            let key = prefix + objKey;

            QueryStringBuilder.PopulateSearchParams(params, key, value);
        }
    }

    private static PopulateSearchParams<T>(params: URLSearchParams, key: string, value: any) {
        if (value instanceof Array) {
            QueryStringBuilder.PopulateArray(params, key, value);
        }
        else if (value instanceof Date) {
            params.set(key, value.toISOString());
        }
        else if (value instanceof Object) {
            QueryStringBuilder.PopulateObject(params, key, value);
        }
        else {
            params.set(key, value?value.toString():'');
        }
    }
}

/** a collection of utilities */
export const UTIL = {
    isNotEmptyString(str: string): boolean {
        return typeof str === "string" && str.length > 0;
    },
    /** get data from response body */
    getResponseBody(resp: Response) {
        return resp.json();
    },
    /** handle Response error */
    handleResponseError(error: Response) {
        // In a real world app, we might use a remote logging infrastructure
        const body = error.json() || '';
        const err = body || JSON.stringify(body);
        const errMsg = `${error.status} - ${error.statusText || ''}\t${err}`;
        debugger;
        // console.error(errMsg);
        return Promise.reject(errMsg);
    },
    /** 
     * constructe URLSearchParams by form data(obejct) 
     * limitation: not support complicated form data, namely the properties of form data should be primitive type
     */
    getURLSearchParams(formData: object): URLSearchParams {
        let params = new URLSearchParams();
        if (formData) {
            Object.keys(formData).forEach(p => {
                if (typeof formData[p] !== "object")
                    params.append(p, formData[p]);
            });
        }
        return params;
    },


    /** deep copy object */
    cloneDeep<T>(value: T): T {
        if (typeof value === "object")
            return JSON.parse(JSON.stringify(value));
        return value;
    },
    /**
     * check if given json(object) contains all keys defined in _class
     * @param json given json type
     * @param _class comparing template
     */
    containsAllKeys(json: any, _class: any) {
        if (typeof json === "object" && (typeof _class === "function" || typeof _class === "object")) {
            if (typeof _class === "function")   _class = new _class();  // create instance if it is function so that we can enumerate its property later
            let sortFx = (p1: string, p2: string): number => {
                if (p1 === p2) return 0;
                else if (p1 < p2) return -1;
                else return 1;
            }
            let props1 = Object.keys(json).sort(sortFx);
            let props2 = Object.keys(_class).sort(sortFx);
            if (props1.length !== props2.length) return false;
            return !props1.some((p, i) => p !== props2[i]);
        } else {
            throw "invalid argument(s)";
        }
    }
}
Object.freeze(UTIL);