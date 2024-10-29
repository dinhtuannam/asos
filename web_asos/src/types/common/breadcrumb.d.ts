type BreadcrumbItem = {
    title: string;
    link: string;
    parent?: string;
};

type NavigateType = {
    [key: string]: BreadcrumbItem;
};
