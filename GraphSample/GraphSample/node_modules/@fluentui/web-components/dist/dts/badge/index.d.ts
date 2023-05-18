import { Badge } from '@microsoft/fast-foundation';
/**
 * Badge appearance options.
 * @public
 */
export declare type BadgeAppearance = 'accent' | 'lightweight' | 'neutral' | string;
/**
 * The Fluent Badge Element. Implements {@link @microsoft/fast-foundation#Badge},
 * {@link @microsoft/fast-foundation#BadgeTemplate}
 *
 *
 * @public
 * @remarks
 * HTML Element: \<fluent-badge\>
 */
export declare class FluentBadge extends Badge {
    appearance: BadgeAppearance;
    private appearanceChanged;
}
/**
 * Styles for Badge
 * @public
 */
export declare const BadgeStyles: import("@microsoft/fast-element").ElementStyles;
