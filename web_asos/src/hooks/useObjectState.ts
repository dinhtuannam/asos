import { useState } from 'react';

// const [data, setData] = useObjectState({
//     username: 'abc',
//     email: 'xyz',
//     password: 'bbb',
// });

function useObjectState<T extends object>(initialState: T) {
    const [state, setState] = useState<T>(initialState);

    const setKeyValue = <K extends keyof T>(key: K, value: T[K]) => {
        setState((prevState) => ({
            ...prevState,
            [key]: value,
        }));
    };

    return [state, setKeyValue, setState] as const;
}

export default useObjectState;
