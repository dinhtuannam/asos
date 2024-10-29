import { combineReducers } from '@reduxjs/toolkit';
import { persistReducer, PersistConfig } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import loading, { loadingInitialState, LoadingState } from './slicers/loading.slice';
import breadcrumb, { breadcrumbInitialState, BreadcrumbState } from './slicers/breadcrumb.slice';
import toast, { toastInitialState, ToastState } from './slicers/toast.slice';
import profile, { profileInitialState, ProfileState } from './slicers/profile.slice';

export type RawRootState = {
    loading: LoadingState;
    breadcrumb: BreadcrumbState;
    toast: ToastState;
    profile: ProfileState;
};

export const allInitialStates: RawRootState = {
    loading: loadingInitialState,
    breadcrumb: breadcrumbInitialState,
    toast: toastInitialState,
    profile: profileInitialState,
};

const rootReducer = combineReducers({
    loading,
    breadcrumb,
    toast,
    profile,
});

export type RootState = ReturnType<typeof rootReducer>;

const persistConfig: PersistConfig<RootState> = {
    key: 'root',
    storage,
    whitelist: ['profile'],
};

const persistedReducer = persistReducer(persistConfig, rootReducer);

export default persistedReducer;
